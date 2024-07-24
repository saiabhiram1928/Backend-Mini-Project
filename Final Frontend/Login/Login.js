import {login} from './UserAuth.js';
document.getElementById('loginForm').addEventListener('submit', (event) => {
    event.preventDefault();
    var email = document.getElementById('email');
    var password = document.getElementById('password');
    var emailError = document.getElementById('emailError');
    var passwordError = document.getElementById('passwordError');

    emailError.classList.add('hidden');
    passwordError.classList.add('hidden');

    var isValid = true;

    if (!email.checkValidity()) {
        emailError.classList.remove('hidden');
        isValid = false;
    }

   
    var passwordPattern = /^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[\W_]).{8,}$/;
    if (!passwordPattern.test(password.value)) {
        passwordError.classList.remove('hidden');
        isValid = false;
    }

    if (isValid) {
       postLoginDateToBackend(email.value,password.value)
    }
});

const postLoginDateToBackend = async (email,password)=>{
    console.log(email,password,"Login Called");
    try {
        const response = await fetch('https://localhost:7029/api/UserAuth/login', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({
                Email: email,
                Password: password
            })
        });
        console.log("fetced");
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
        console.error('Error:', error);
        alert(error.message);
    }
}