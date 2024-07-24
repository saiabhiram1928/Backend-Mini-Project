const validatePassword = ()=> {
    var password = document.getElementById('password').value;
    var length = document.getElementById('length');
    var uppercase = document.getElementById('uppercase');
    var number = document.getElementById('number');
    var special = document.getElementById('special');

    // Regular expressions for different criteria
    var lengthPattern = /.{8,}/;
    var uppercasePattern = /[A-Z]/;
    var numberPattern = /\d/;
    var specialPattern = /[\W_]/;

    // Check each criterion and update classes accordingly
    if (lengthPattern.test(password)) {
        length.classList.remove('invalid');
        length.classList.add('valid');
    } else {
        length.classList.remove('valid');
        length.classList.add('invalid');
    }

    if (uppercasePattern.test(password)) {
        uppercase.classList.remove('invalid');
        uppercase.classList.add('valid');
    } else {
        uppercase.classList.remove('valid');
        uppercase.classList.add('invalid');
    }

    if (numberPattern.test(password)) {
        number.classList.remove('invalid');
        number.classList.add('valid');
    } else {
        number.classList.remove('valid');
        number.classList.add('invalid');
    }

    if (specialPattern.test(password)) {
        special.classList.remove('invalid');
        special.classList.add('valid');
    } else {
        special.classList.remove('valid');
        special.classList.add('invalid');
    }
}

const validateConfirmPassword = () => {
    var password = document.getElementById('password').value;
    var confirmPassword = document.getElementById('confirmPassword').value;
    var confirmPasswordError = document.getElementById('confirmPasswordError');

    if (password === confirmPassword) {
        confirmPasswordError.classList.add('hidden');
    } else {
        confirmPasswordError.classList.remove('hidden');
    }
}