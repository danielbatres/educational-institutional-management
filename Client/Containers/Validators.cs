using System.Text.RegularExpressions;

namespace edu_institutional_management.Client.Containers;

public class Validators {
    public bool MaxMinLength(string text, int length, string maxmin) {
        switch (maxmin) {
            case "max":
                if (text.Length > length) return true;
                break;
            case "min":
                if (text.Length < length) return true;
                break;
        }

        return false;
    }

    public bool IsValidEmail(string email) {
        string pattern = @"^([a-zA-Z0-9._%+-]+)@([a-zA-Z0-9.-]+)\.([a-zA-Z]{2,})$";

        return Regex.IsMatch(email, pattern);
    }

    public PasswordStrength IsValidPassword(string password) {
        int score = 0;

        if (password.Length >= 8) score++;

        if (HasUpperCaseLetter(password) && HasLowerCaseLetter(password)) score++;

        if (HasDigit(password)) score++;

        if (HasSpecialCharacter(password)) score++;

        if (score < 2)
            return PasswordStrength.VeryWeak;
        else if (score < 3)
            return PasswordStrength.Weak;
        else if (score < 4)
            return PasswordStrength.Moderate;
        else if (score < 5)
            return PasswordStrength.Strong;
        else
            return PasswordStrength.VeryStrong;
    }

    private bool HasUpperCaseLetter(string password) {
        return !string.IsNullOrEmpty(password) && password.Any(char.IsUpper);
    }

    private bool HasLowerCaseLetter(string password) {
        return !string.IsNullOrEmpty(password) && password.Any(char.IsLower);
    }

    private bool HasDigit(string password) {
        return !string.IsNullOrEmpty(password) && password.Any(char.IsDigit);
    }

    private bool HasSpecialCharacter(string password) {
        return !string.IsNullOrEmpty(password) && password.Any(c => !char.IsLetterOrDigit(c));
    }
}

public enum PasswordStrength {
    VeryWeak,
    Weak,
    Moderate,
    Strong,
    VeryStrong
}