
// // Simulate authentication state
// const isAuthenticated = true; // Change this to true to simulate a logged-in state
// const navbarAuth = document.getElementById('navbar-auth');
// if (isAuthenticated) {
//     console.log("hii");
//     navbarAuth.innerHTML = `
//         <div id="cart">
//             <a href="/cart" class="bg-gray-700 px-4 py-2 rounded-md">Cart</a>
//         </div>
//         <div id="profile-dropdown" class="relative">
//             <button class="bg-gray-700 px-4 py-2 rounded-md">Profile</button>
//             <div class="absolute right-0 mt-2 bg-gray-800 rounded-md shadow-lg py-2 w-48">
//                 <a href="/profile" class="block px-4 py-2 text-sm text-white">My Profile</a>
//                 <a href="/logout" class="block px-4 py-2 text-sm text-white">Logout</a>
//             </div>
//         </div>
//     `;
// } else {
//     console.log("3");
//     navbarAuth.innerHTML = '<a href="/login" class="bg-red-500 px-4 py-2 rounded-md">Login</a>';
// }

document.addEventListener('DOMContentLoaded', function() {
    document.getElementById('hamburger-menu').addEventListener('click', function () {
        const mobileSearchForm = document.getElementById('mobile-search-form');
        mobileSearchForm.classList.toggle('hidden');
    });
});

document.getElementById('search-form').addEventListener('submit', function (event) {
    event.preventDefault();
    const searchText = document.getElementById('search-text').value;
    const searchGenre = document.getElementById('search-genre').value;
    console.log(searchGenre ,searchText);
    // window.location.href = `searchResults.html?search=${encodeURIComponent(searchText)}&genre=${encodeURIComponent(searchGenre)}`;
});

document.getElementById('mobile-search-form').addEventListener('submit', function (event) {
    event.preventDefault();
    const searchText = document.getElementById('mobile-search-text').value;
    const searchGenre = document.getElementById('mobile-search-genre').value;
    console.log(searchText ,searchGenre);
    // window.location.href = `searchResults.html?search=${encodeURIComponent(searchText)}&genre=${encodeURIComponent(searchGenre)}`;
});

// Movie data example
const movies = [
    { id: 1, title: "Movie 1", image: "https://images.pexels.com/photos/3920974/pexels-photo-3920974.jpeg?cs=srgb&dl=pexels-dmitry-demidov-515774-3920974.jpg&fm=jpg", releaseDate: "2021-01-01", director: "Director 1", genre: "Action" },
    { id: 2, title: "Movie 2", image: "https://images.pexels.com/photos/3920974/pexels-photo-3920974.jpeg?cs=srgb&dl=pexels-dmitry-demidov-515774-3920974.jpg&fm=jpg", releaseDate: "2021-01-02", director: "Director 2", genre: "Drama" },
    { id: 3, title: "Movie 3", image: "https://images.pexels.com/photos/3920974/pexels-photo-3920974.jpeg?cs=srgb&dl=pexels-dmitry-demidov-515774-3920974.jpg&fm=jpg", releaseDate: "2021-01-03", director: "Director 3", genre: "Sci-Fi" },
    { id: 4, title: "Movie 4", image: "https://images.pexels.com/photos/3920974/pexels-photo-3920974.jpeg?cs=srgb&dl=pexels-dmitry-demidov-515774-3920974.jpg&fm=jpg", releaseDate: "2021-01-04", director: "Director 4", genre: "Sci-Fi" },
    { id: 5, title: "Movie 5", image: "https://images.pexels.com/photos/3920974/pexels-photo-3920974.jpeg?cs=srgb&dl=pexels-dmitry-demidov-515774-3920974.jpg&fm=jpg", releaseDate: "2021-01-05", director: "Director 5", genre: "Drama" },
    { id: 6, title: "Movie 6", image: "https://images.pexels.com/photos/3920974/pexels-photo-3920974.jpeg?cs=srgb&dl=pexels-dmitry-demidov-515774-3920974.jpg&fm=jpg", releaseDate: "2021-01-06", director: "Director 6", genre: "Thriller" },
    { id: 7, title: "Movie 7", image: "https://images.pexels.com/photos/3920974/pexels-photo-3920974.jpeg?cs=srgb&dl=pexels-dmitry-demidov-515774-3920974.jpg&fm=jpg", releaseDate: "2021-01-07", director: "Director 7", genre: "Drama" },
    { id: 8, title: "Movie 8", image: "https://images.pexels.com/photos/3920974/pexels-photo-3920974.jpeg?cs=srgb&dl=pexels-dmitry-demidov-515774-3920974.jpg&fm=jpg", releaseDate: "2021-01-08", director: "Director 8", genre: "Action" },
    { id: 9, title: "Movie 9", image: "https://images.pexels.com/photos/3920974/pexels-photo-3920974.jpeg?cs=srgb&dl=pexels-dmitry-demidov-515774-3920974.jpg&fm=jpg", releaseDate: "2021-01-09", director: "Director 9", genre: "Drama" },
    { id: 10, title: "Movie 10", image: "https://images.pexels.com/photos/3920974/pexels-photo-3920974.jpeg?cs=srgb&dl=pexels-dmitry-demidov-515774-3920974.jpg&fm=jpg", releaseDate: "2021-01-10", director: "Director 10", genre: "Sci-Fi" },
    { id: 11, title: "Movie 11", image: "https://images.pexels.com/photos/3920974/pexels-photo-3920974.jpeg?cs=srgb&dl=pexels-dmitry-demidov-515774-3920974.jpg&fm=jpg", releaseDate: "2021-01-11", director: "Director 11", genre: "Sci-Fi" },
    { id: 12, title: "Movie 12", image: "https://images.pexels.com/photos/3920974/pexels-photo-3920974.jpeg?cs=srgb&dl=pexels-dmitry-demidov-515774-3920974.jpg&fm=jpg", releaseDate: "2021-01-12", director: "Director 12", genre: "Drama" },
    { id: 13, title: "Movie 13", image: "https://images.pexels.com/photos/3920974/pexels-photo-3920974.jpeg?cs=srgb&dl=pexels-dmitry-demidov-515774-3920974.jpg&fm=jpg", releaseDate: "2021-01-13", director: "Director 13", genre: "Thriller" },
    { id: 14, title: "Movie 14", image: "https://images.pexels.com/photos/3920974/pexels-photo-3920974.jpeg?cs=srgb&dl=pexels-dmitry-demidov-515774-3920974.jpg&fm=jpg", releaseDate: "2021-01-14", director: "Director 14", genre: "Drama" },
    { id: 15, title: "Movie 15", image: "https://images.pexels.com/photos/3920974/pexels-photo-3920974.jpeg?cs=srgb&dl=pexels-dmitry-demidov-515774-3920974.jpg&fm=jpg", releaseDate: "2021-01-15", director: "Director 15", genre: "Action" },
    { id: 16, title: "Movie 16", image: "https://images.pexels.com/photos/3920974/pexels-photo-3920974.jpeg?cs=srgb&dl=pexels-dmitry-demidov-515774-3920974.jpg&fm=jpg", releaseDate: "2021-01-16", director: "Director 16", genre: "Drama" },
    { id: 17, title: "Movie 17", image: "https://images.pexels.com/photos/3920974/pexels-photo-3920974.jpeg?cs=srgb&dl=pexels-dmitry-demidov-515774-3920974.jpg&fm=jpg", releaseDate: "2021-01-17", director: "Director 17", genre: "Sci-Fi" },
    { id: 18, title: "Movie 18", image: "https://images.pexels.com/photos/3920974/pexels-photo-3920974.jpeg?cs=srgb&dl=pexels-dmitry-demidov-515774-3920974.jpg&fm=jpg", releaseDate: "2021-01-18", director: "Director 18", genre: "Sci-Fi" },
    { id: 19, title: "Movie 19", image: "https://images.pexels.com/photos/3920974/pexels-photo-3920974.jpeg?cs=srgb&dl=pexels-dmitry-demidov-515774-3920974.jpg&fm=jpg", releaseDate: "2021-01-19", director: "Director 19", genre: "Drama" },
    { id: 20, title: "Movie 20", image: "https://images.pexels.com/photos/3920974/pexels-photo-3920974.jpeg?cs=srgb&dl=pexels-dmitry-demidov-515774-3920974.jpg&fm=jpg", releaseDate: "2021-01-20", director: "Director 20", genre: "Thriller" },
    { id: 21, title: "Movie 21", image: "https://images.pexels.com/photos/3920974/pexels-photo-3920974.jpeg?cs=srgb&dl=pexels-dmitry-demidov-515774-3920974.jpg&fm=jpg", releaseDate: "2021-01-21", director: "Director 21", genre: "Drama" },
    { id: 22, title: "Movie 22", image: "https://images.pexels.com/photos/3920974/pexels-photo-3920974.jpeg?cs=srgb&dl=pexels-dmitry-demidov-515774-3920974.jpg&fm=jpg", releaseDate: "2021-01-22", director: "Director 22", genre: "Action" },
    { id: 23, title: "Movie 23", image: "https://images.pexels.com/photos/3920974/pexels-photo-3920974.jpeg?cs=srgb&dl=pexels-dmitry-demidov-515774-3920974.jpg&fm=jpg", releaseDate: "2021-01-23", director: "Director 23", genre: "Drama" },
    { id: 24, title: "Movie 24", image: "https://images.pexels.com/photos/3920974/pexels-photo-3920974.jpeg?cs=srgb&dl=pexels-dmitry-demidov-515774-3920974.jpg&fm=jpg", releaseDate: "2021-01-24", director: "Director 24", genre: "Sci-Fi" },
    { id: 25, title: "Movie 25", image: "https://images.pexels.com/photos/3920974/pexels-photo-3920974.jpeg?cs=srgb&dl=pexels-dmitry-demidov-515774-3920974.jpg&fm=jpg", releaseDate: "2021-01-25", director: "Director 25", genre: "Sci-Fi" },
    { id: 26, title: "Movie 26", image: "https://images.pexels.com/photos/3920974/pexels-photo-3920974.jpeg?cs=srgb&dl=pexels-dmitry-demidov-515774-3920974.jpg&fm=jpg", releaseDate: "2021-01-26", director: "Director 26", genre: "Drama" },
    { id: 27, title: "Movie 27", image: "https://images.pexels.com/photos/3920974/pexels-photo-3920974.jpeg?cs=srgb&dl=pexels-dmitry-demidov-515774-3920974.jpg&fm=jpg", releaseDate: "2021-01-27", director: "Director 27", genre: "Thriller" },
    { id: 28, title: "Movie 28", image: "https://images.pexels.com/photos/3920974/pexels-photo-3920974.jpeg?cs=srgb&dl=pexels-dmitry-demidov-515774-3920974.jpg&fm=jpg", releaseDate: "2021-01-28", director: "Director 28", genre: "Drama" },
    { id: 29, title: "Movie 29", image: "https://images.pexels.com/photos/3920974/pexels-photo-3920974.jpeg?cs=srgb&dl=pexels-dmitry-demidov-515774-3920974.jpg&fm=jpg", releaseDate: "2021-01-29", director: "Director 29", genre: "Action" },
    { id: 30, title: "Movie 30", image: "https://images.pexels.com/photos/3920974/pexels-photo-3920974.jpeg?cs=srgb&dl=pexels-dmitry-demidov-515774-3920974.jpg&fm=jpg", releaseDate: "2021-01-30", director: "Director 30", genre: "Drama" },
    { id: 31, title: "Movie 31", image: "https://images.pexels.com/photos/3920974/pexels-photo-3920974.jpeg?cs=srgb&dl=pexels-dmitry-demidov-515774-3920974.jpg&fm=jpg", releaseDate: "2021-01-31", director: "Director 31", genre: "Sci-Fi" },
    { id: 32, title: "Movie 32", image: "https://images.pexels.com/photos/3920974/pexels-photo-3920974.jpeg?cs=srgb&dl=pexels-dmitry-demidov-515774-3920974.jpg&fm=jpg", releaseDate: "2021-02-01", director: "Director 32", genre: "Sci-Fi" },
    { id: 33, title: "Movie 33", image: "https://images.pexels.com/photos/3920974/pexels-photo-3920974.jpeg?cs=srgb&dl=pexels-dmitry-demidov-515774-3920974.jpg&fm=jpg", releaseDate: "2021-02-02", director: "Director 33", genre: "Drama" },
    { id: 34, title: "Movie 34", image: "https://images.pexels.com/photos/3920974/pexels-photo-3920974.jpeg?cs=srgb&dl=pexels-dmitry-demidov-515774-3920974.jpg&fm=jpg", releaseDate: "2021-02-03", director: "Director 34", genre: "Thriller" },
    { id: 35, title: "Movie 35", image: "https://images.pexels.com/photos/3920974/pexels-photo-3920974.jpeg?cs=srgb&dl=pexels-dmitry-demidov-515774-3920974.jpg&fm=jpg", releaseDate: "2021-02-04", director: "Director 35", genre: "Drama" },
    { id: 36, title: "Movie 36", image: "https://images.pexels.com/photos/3920974/pexels-photo-3920974.jpeg?cs=srgb&dl=pexels-dmitry-demidov-515774-3920974.jpg&fm=jpg", releaseDate: "2021-02-05", director: "Director 36", genre: "Action" },
    { id: 37, title: "Movie 37", image: "https://images.pexels.com/photos/3920974/pexels-photo-3920974.jpeg?cs=srgb&dl=pexels-dmitry-demidov-515774-3920974.jpg&fm=jpg", releaseDate: "2021-02-06", director: "Director 37", genre: "Drama" },
    { id: 38, title: "Movie 38", image: "https://images.pexels.com/photos/3920974/pexels-photo-3920974.jpeg?cs=srgb&dl=pexels-dmitry-demidov-515774-3920974.jpg&fm=jpg", releaseDate: "2021-02-07", director: "Director 38", genre: "Sci-Fi" },
    { id: 39, title: "Movie 39", image: "https://images.pexels.com/photos/3920974/pexels-photo-3920974.jpeg?cs=srgb&dl=pexels-dmitry-demidov-515774-3920974.jpg&fm=jpg", releaseDate: "2021-02-08", director: "Director 39", genre: "Sci-Fi" },
    { id: 40, title: "Movie 40", image: "https://images.pexels.com/photos/3920974/pexels-photo-3920974.jpeg?cs=srgb&dl=pexels-dmitry-demidov-515774-3920974.jpg&fm=jpg", releaseDate: "2021-02-09", director: "Director 40", genre: "Drama" }
];


let currentPage = 1;
const moviesPerPage = 9;

// Function to get URL parameter
const getUrlParameter = (name)  => {
    name = name.replace(/[\[]/, '\\[').replace(/[\]]/, '\\]');
    const regex = new RegExp('[\\?&]' + name + '=([^&#]*)');
    const results = regex.exec(location.search);
    return results === null ? null : decodeURIComponent(results[1].replace(/\+/g, ' '));
}

// Set initial currentPage based on URL parameter
const currentPageText = document.getElementById('current-page-number')
const pageParameter = getUrlParameter('page');
if (pageParameter) {
    console.log(pageParameter);
    currentPage = parseInt(pageParameter, 10) || 1;
    currentPageText.innerHTML = pageParameter
}

function displayMovies() {
    const movieGrid = document.getElementById('movie-grid');
    movieGrid.innerHTML = ''; // Clear previous movie cards

    const startIndex = (currentPage - 1) * moviesPerPage;
    const endIndex = startIndex + moviesPerPage;
    const currentMovies = movies.slice(startIndex, endIndex);

    currentMovies.forEach(movie => {
        const movieCard = document.createElement('div');
        movieCard.classList.add('container', 'mx-auto', 'p-9','bg-black' , 'max-w-sm' , 
            'rounded-2xl', 'overflow-hidden' , 'shadow-md', 'hover:shadow-2xl', 'hover:shadow-red-600',
            'transition', 'duration-300' , 'z-5'
        );

        movieCard.innerHTML = `  
    <img class="rounded-xl glitch-text" src=${movie.image} alt=${movie.title} />
    <div class="flex justify-between items-center">
        <div>
            <h1 class="mt-5 text-2xl font-semibold">${movie.title}</h1>
            <p class="mt-2">$11.99</p>
        </div>
        <div>
            <button class="btn btn-accent ">More info</button>
        </div>
    </div>
    <div class= "mt-5 flex justify-end items-center p-2"> 
      <div class = "badge badge-outline text-cyan-400 mr-5">${movie.director} </div>
         <div class = "badge badge-outline text-green-400 mr-5">${movie.genre} </div>
          <div class = "badge badge-outline text-blue-400">${movie.releaseDate} </div>
     </div>
        `;
        movieGrid.appendChild(movieCard);
    });

    // Update URL without reloading the page
    window.history.pushState({}, '', `?page=${currentPage}`);
}

document.getElementById('prev-page').addEventListener('click', () => {
    if (currentPage > 1) {
        currentPage--;
        currentPageText.innerText = currentPage.toString();
        displayMovies();
    }
});

document.getElementById('next-page').addEventListener('click', () => {
    if (currentPage * moviesPerPage < movies.length) {
        currentPage++;
        currentPageText.innerText = currentPage.toString();
        displayMovies();
    }
});

// Initial movie display
displayMovies();
