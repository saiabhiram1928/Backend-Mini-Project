import { getCookie } from './Helper.js';

document.addEventListener('DOMContentLoaded',  () => {
    document.getElementById('hamburger-menu').addEventListener('click', function () {
        const mobileSearchForm = document.getElementById('mobile-search-form');
        mobileSearchForm.classList.toggle('hidden');
    });

    const searchForm = document.getElementById('search-form');
    const mobileSearchForm = document.getElementById('mobile-search-form');
    searchForm.addEventListener('submit', handleSearch);
    mobileSearchForm.addEventListener('submit', handleSearch);
});

const handleSearch = (event) =>{
    event.preventDefault();
    const searchText = event.target.querySelector('input').value;
    const searchGenre = event.target.querySelector('select').value;
    window.location.href = `searchResults.html?search=${encodeURIComponent(searchText)}`;
}

const isAuthenticated = getCookie('isLoggedIn') || false;
const navbarAuth = document.getElementById('navbar-end');
if (isAuthenticated) {
    navbarAuth.innerHTML += `
        <!-- Cart icon with badge showing number of items -->
        <div tabindex="0" role="button" class="relative ">
            <svg xmlns="http://www.w3.org/2000/svg" class="h-6 w-6" fill="none" viewBox="0 0 24 24" stroke="currentColor">
                <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M3 3h2l.4 2M7 13h10l4-8H5.4M7 13L5.4 5M7 13l-2.293 2.293c-.63.63-.184 1.707.707 1.707H17m0 0a2 2 0 100 4 2 2 0 000-4zm-8 2a2 2 0 11-4 0 2 2 0 014 0z"></path>
            </svg>
            <span id="cart-item-total" class="absolute -top-2 -right-2 inline-flex items-center justify-center px-2 py-1 text-xs font-bold leading-none text-red-100 bg-red-600 rounded-full">8</span>
        </div>
        <!-- profile icon drop with contents like signout, profile, myorders -->
        <div class="dropdown dropdown-end z-50 ">
            <label tabindex="0" class="cursor-pointer">
                <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="currentColor" class="h-6 w-6">
                    <path fill-rule="evenodd" d="M18.685 19.097A9.723 9.723 0 0021.75 12c0-5.385-4.365-9.75-9.75-9.75S2.25 6.615 2.25 12c0 2.427.906 4.64 2.4 6.324A9.724 9.724 0 0012 21.75c2.566 0 4.895-1.035 6.685-2.653zM12 13.5a3 3 0 100-6 3 3 0 000 6zm-4.5 5.25a6.75 6.75 0 0113.5 0v.278a9.715 9.715 0 01-13.5 0V18.75z" clip-rule="evenodd" />
                </svg>
            </label>
            <ul tabindex="0" class="dropdown-content menu p-2 shadow bg-base-100 rounded-box w-52">
                <li><a>My Profile</a></li>
                <li><a>My Orders</a></li>
                <li id="signout"><a>Sign Out</a></li>
            </ul>
        </div>
    `;
    // document.getElementById('signout').addEventListener('click', function () {
    //     document.cookie = "isLoggedIn=; expires=Thu, 01 Jan 1970 00:00:00 UTC; path=/;";
    //     document.cookie = "cartItems=; expires=Thu, 01 Jan 1970 00:00:00 UTC; path=/;";
    //     location.reload();
    // });
} else {
    navbarAuth.innerHTML += `<a href="signin.html" class="btn btn-accent rounded-lg">Sign In</a>`;
}

const fetchMovies = async () => {
    const searchParams = new URLSearchParams(window.location.search);
    const search = searchParams.get('search') || '';
    const genre = searchParams.get('genre') || '';


    const url = `https://localhost:7029/api/Video/Search?name=${encodeURIComponent(search)}`;

    try {
        const response = await fetch(url, {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json'
            },
        });

        if (!response.ok && response.status != 404) {
            const errorData = await response.json();
            console.log(errorData);
            alert('Failed to fetch movies: ' + errorData.message);
            return;
        }

        const moviesItems = await response.json();
        if(Array.isArray(moviesItems)) displayMovies(moviesItems);
        else displayErrorMessage(moviesItems)
    } catch (error) {
        console.log(error);
        alert("Something went wrong while fetching movies.");
    }
};
var movies = []
const displayMovies = (moviesItems) => {
    const movieGrid = document.getElementById('movie-grid');
    movieGrid.innerHTML = '';
    movies = moviesItems;
    movies.forEach(movie => {
        const movieCard = document.createElement('div');
        movieCard.classList.add('container', 'mx-auto', 'p-7', 'bg-black', 'max-w-sm', 'rounded-2xl', 'overflow-hidden', 'shadow-md', 'hover:shadow-2xl', 'hover:shadow-red-600', 'transition', 'duration-300', 'z-5');

        movieCard.innerHTML = `
            <img class="rounded-xl glitch-text" src="${movie.image}" alt="${movie.tittle}" />
            <div class="flex justify-between items-center">
                <div>
                    <h1 class="mt-5 text-2xl font-semibold">${movie.tittle}</h1>
                    <p class="mt-2 text-xl">₹${movie.price}</p>
                </div>
                <div>
                    <button class="btn btn-accent">More info</button>
                </div>
            </div>
            <div class="mt-5 flex justify-between items-center">
                <kbd class="kbd md:kbd-md border-2 w-1/2 bg-black text-green-400 mr-2">${movie.genre}</kbd>
                <kbd class="kbd md:kbd-md border-2 w-1/2 bg-black text-cyan-400 mr-2">${movie.releaseDate}</kbd>
            </div>
        `;
        movieGrid.appendChild(movieCard);
    });
};
const displayErrorMessage = (error)=>{
    console.log(error);
    const movieGrid = document.getElementById('movie-grid');
    console.log("hii");
    movieGrid.innerHTML = `<h1 class ="text-lg text-center text-white"> ${error.message} </h1>`
}
document.addEventListener('DOMContentLoaded', () => {
    fetchMovies();
});

// const updateUrlAndFetchData = ({ search = '', genre = ''} = {}) => {
//     const url = new URL(window.location);
//     url.searchParams.set('search', search);
//     window.history.pushState({}, '', url);
//     fetchMovies();
// };
