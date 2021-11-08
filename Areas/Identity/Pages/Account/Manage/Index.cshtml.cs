using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MvcMovie.Models;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace MvcMovie.Areas.Identity.Pages.Account.Manage
{
    public partial class IndexModel : PageModel
    {
        private readonly UserManager<Pengguna> _userManager;
        private readonly SignInManager<Pengguna> _signInManager;

        public IndexModel(
            UserManager<Pengguna> userManager,
            SignInManager<Pengguna> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public string Username { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }
        public string Avatar { get; set; }

        public IActionResult UploadFile(IFormFile file)
        {
        return Ok();
        }

        private IActionResult Ok()
        {
            throw new NotImplementedException();
        }

        public class InputModel
        {
         [Phone]
         [Display(Name = "Phone number")]
         public string PhoneNumber { get; set; }
         public IFormFile AvatarFile { get; set; }
        }
        private async Task LoadAsync(Pengguna user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);

            Username = userName;

            Input = new InputModel
            {
                PhoneNumber = phoneNumber
            };
            Avatar = Path.Combine("/Profile/Avatar", user.Avatar ?? "");
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            if (Input.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    StatusMessage = "Unexpected error when trying to set phone number.";
                    return RedirectToPage();
                }
            }

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();

            var avatarDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Avatars");
            Directory.CreateDirectory(avatarDirectory);

            var extension = Path.GetExtension(Input.AvatarFile?.FileName)?.ToLowerInvariant();
            var permittedType = new string[] { ".png", ".jpg" };
            if (string.IsNullOrEmpty(extension) || !permittedType.Contains(extension))
            {
             StatusMessage = "Unsupported file type";
             return RedirectToPage();
            }
            
            var fileName = $"{user.Id}{extension}";
            var avatarFile = Path.Combine(avatarDirectory, fileName);
            using var stream = new FileStream(avatarFile, FileMode.Create);
            await Input.AvatarFile.CopyToAsync(stream);
            user.Avatar = fileName;
            await _userManager.UpdateAsync(user);

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }
    }
}