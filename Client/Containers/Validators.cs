using Microsoft.AspNetCore.Components.WebAssembly.Http;

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

    public bool CheckEmail() {
        return false;
    }

    public bool CheckPassword() {
        return false;
    }
}