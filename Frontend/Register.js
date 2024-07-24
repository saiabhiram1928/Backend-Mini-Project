import {login} from './UserAuth.js';
document.getElementById('registerForm').addEventListener('submit', (event) => {
            event.preventDefault();
            var firstName = document.getElementById('firstName');
            var lastName = document.getElementById('lastName');
            var email = document.getElementById('email');
            var password = document.getElementById('password');
            var confirmPassword = document.getElementById('confirmPassword');
            var firstNameError = document.getElementById('firstNameError');
            var lastNameError = document.getElementById('lastNameError');
            var emailError = document.getElementById('emailError');
            var passwordError = document.getElementById('passwordError');
            var confirmPasswordError = document.getElementById('confirmPasswordError');

            firstNameError.classList.add('hidden');
            lastNameError.classList.add('hidden');
            emailError.classList.add('hidden');
            passwordError.classList.add('hidden');
            confirmPasswordError.classList.add('hidden');

            var isValid = true;

            if (!firstName.checkValidity()) {
                firstNameError.classList.remove('hidden');
                isValid = false;
            }

            if (!lastName.checkValidity()) {
                lastNameError.classList.remove('hidden');
                isValid = false;
            }

            if (!email.checkValidity()) {
                emailError.classList.remove('hidden');
                isValid = false;
            }

            
            var passwordPattern = /^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[\W_]).{8,}$/;
            if (!passwordPattern.test(password.value)) {
                passwordError.classList.remove('hidden');
                isValid = false;
            }

            if (password.value !== confirmPassword.value) {
                confirmPasswordError.classList.remove('hidden');
                isValid = false;
            }

            if (isValid) {
                postRegisterToBackend(firstName.value , lastName.value , password.value ,email.value);
            }
});
const postRegisterToBackend =async (firstName, lastName,password,email) =>{
    console.log(firstName,lastName,password,email);
    try {
        const response = await fetch('https://localhost:7029/api/UserAuth/Register', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({
                FirstName: firstName,
                LastName: lastName,
                Email: email,
                Password: password
            })
        });

        if (!response.ok) {
            const errorData = await response.json();
            console.log(errorData);
            alert('Registration Failed'+errorData.message)
            return;
        }

        const data = await response.json();
        console.log('Registration successful:', data);
        window.location.href = 'http://127.0.0.1:5500/Home.html'
        login(data); 
    } catch (error) {
        console.error('Error:', error.message);
        alert(error.message);
    }
}

