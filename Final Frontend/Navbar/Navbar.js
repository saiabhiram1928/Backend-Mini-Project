import {getCookie , fetchWithAuth , removeCookie} from './Helper.js';
const isAuthenticated = getCookie('isLoggedIn') || false; 
const navbarAuth = document.getElementById('navbar-end');
const role = getCookie('role') 
console.log(isAuthenticated,"isAuthenticated" , role);
if (isAuthenticated) {
    let count = 0;
    try {
        const response = await fetchWithAuth(`https://localhost:7029/api/Cart/CartItemCount`, {
            method: 'GET'
        });
        if (!response.ok) {
            console.log(response);
            count = 0;
        } else {
            count = await response.json();
        }
    } catch (error) {
        console.error('Cart Count unable to access:', error);
    }
    
    navbarAuth.innerHTML += `
    <!-- Cart icon with badge showing number of items -->
    <a href="#" id="cart-link" tabindex="0" role="button" class="relative">
        <svg xmlns="http://www.w3.org/2000/svg" class="h-6 w-6" fill="none" viewBox="0 0 24 24" stroke="currentColor">
            <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M3 3h2l.4 2M7 13h10l4-8H5.4M7 13L5.4 5M7 13l-2.293 2.293c-.63.63-.184 1.707.707 1.707H17m0 0a2 2 0 100 4 2 2 0 000-4zm-8 2a2 2 0 11-4 0 2 2 0 014 0z"></path>
        </svg>
        <span id="cart-item-total" class="absolute -top-2 -right-2 inline-flex items-center justify-center px-2 py-1 text-xs font-bold leading-none text-red-100 bg-red-600 rounded-full">${count}</span>
    </a>
    <!-- Profile icon dropdown with contents like signout, profile, myorders -->
    <div class="dropdown dropdown-end z-50">
        <label tabindex="0" class="cursor-pointer">
            <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="currentColor" class="h-6 w-6">
                <path fill-rule="evenodd" d="M18.685 19.097A9.723 9.723 0 0021.75 12c0-5.385-4.365-9.75-9.75-9.75S2.25 6.615 2.25 12a9.723 9.723 0 003.065 7.097A9.716 9.716 0 0012 21.75a9.716 9.716 0 006.685-2.653zm-12.54-1.285A7.486 7.486 0 0112 15a7.486 7.486 0 015.855 2.812A8.224 8.224 0 0112 20.25a8.224 8.224 0 01-5.855-2.438zm7.605-8.562a3.75 3.75 0 11-7.5 0 3.75 3.75 0 017.5 0z" clip-rule="evenodd" />
            </svg>
        </label>
        <ul tabindex="0" class="dropdown-content menu p-2 shadow bg-red-700 rounded-box w-40">
            <li><a class="text-white link-success" href="Profile.html">Profile</a></li>
            ${role === 'Admin' ? `<li><a class="text-white link-success" href="CreateItem.html">Create Item</a></li>` : ''}
            <li id="sign-out-button" > <span class="cursor-pointer text-white"> Sign out </span></li>
        </ul>
    </div>
`;
    document.getElementById('sign-out-button').addEventListener('click', () => {
        SignOut();
    });

    // Prevent default action and alert user if no items in cart
    document.getElementById('cart-link').addEventListener('click', (event) => {
        if (count == 0) {
            event.preventDefault();
            alert("No Items in Cart");
        } else {
            window.location.href = 'Checkout.html';
        }
    });
}else {
    
    navbarAuth.innerHTML = `<a href = "Login.html" class="btn hover:bg-red-800 border-none bg-red-500 text-white flex">
                Login
                <svg xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24" strokeWidth={1.5} stroke="currentColor" class="h-6 w-6">
                    <path strokeLinecap="round" strokeLinejoin="round" d="M17.25 8.25 21 12m0 0-3.75 3.75M21 12H3" />
                </svg>
             </a>`+navbarAuth.innerHTML;
}
const SignOut = () =>{
    removeCookie('token')
    removeCookie('isLoggedIn')
    removeCookie('role')
    window.location.href = '/Home.html?page=1'
}
document.getElementById('hamburger-menu').addEventListener('click',  () => {
    const mobileSearchForm = document.getElementById('mobile-search-form');
    mobileSearchForm.classList.toggle('hidden');
});
document.addEventListener('DOMContentLoaded', ()=> {
   console.log("hii");
});

document.getElementById('search-form').addEventListener('submit',  (event) =>{
    event.preventDefault();
    const searchText = document.getElementById('search-text').value;
    const searchGenre = document.getElementById('search-genre').value;
    console.log(searchGenre ,searchText);
    window.location.href = `SearchResults.html?search=${encodeURIComponent(searchText)}`;
});

document.getElementById('mobile-search-form').addEventListener('submit',  (event) => {
    event.preventDefault();
    const searchText = document.getElementById('mobile-search-text').value;
    console.log(searchText );
    window.location.href = `searchResults.html?search=${encodeURIComponent(searchText)}`;
});
