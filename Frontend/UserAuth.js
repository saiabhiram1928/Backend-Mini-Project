import {setCookie} from './Helper.js';
export const login = (data) => {
    setCookie('token', data.token, 2);
    setCookie('isLoggedIn', true, 2); 
    setCookie('role' , data.role ,2)
    localStorage.setItem('userDetails', JSON.stringify({
        email: data.email,
        firstName: data.firstName,
        lastName: data.lastName,
        verified: data.verified,
        membershipType: data.membershipType,
        role: data.role
    }));
};

