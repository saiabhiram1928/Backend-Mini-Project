

var movies = [
 
];
let Totalcount; 

let currentPage = 1;
const moviesPerPage = 9;


const getUrlParameter = (name)  => {
    name = name.replace(/[\[]/, '\\[').replace(/[\]]/, '\\]');
    const regex = new RegExp('[\\?&]' + name + '=([^&#]*)');
    const results = regex.exec(location.search);
    return results === null ? null : decodeURIComponent(results[1].replace(/\+/g, ' '));
}


const currentPageText = document.getElementById('current-page-number')
const pageParameter = getUrlParameter('page');
if (pageParameter) {
    console.log(pageParameter);
    currentPage = parseInt(pageParameter, 10) || 1;
    currentPageText.innerHTML = pageParameter
}

const displayMovies = async () => {
    const movieGrid = document.getElementById('movie-grid');
    movieGrid.innerHTML = ''; 
    const moviesItems = await fetchMovies();
    console.log(moviesItems);
    movies = moviesItems.items;
    Totalcount = moviesItems.totalItems;
    const maxpages =  Math.ceil(Totalcount / moviesPerPage);
    console.log(maxpages ,currentPage);
    document.getElementById('next-page').classList.remove('btn-disabled');
    if(currentPage == maxpages){
        document.getElementById('next-page').classList.add('btn-disabled');
    }
    movies.forEach(movie => {
        const movieCard = document.createElement('div');
        movieCard.classList.add('container', 'mx-auto', 'p-7','bg-black' , 'max-w-sm' , 
            'rounded-2xl', 'overflow-hidden' , 'shadow-md', 'hover:shadow-2xl', 'hover:shadow-red-600',
            'transition', 'duration-300' , 'z-5' , 'glitch-card'
        );

        movieCard.innerHTML = `  
    <img class="rounded-xl glitch-text" src="./Rendering Server/uploads/${movie.tittle}_0.png" alt=${movie.tittle} />
    <div class="flex justify-between items-center" >
        <div>
            <h1 class="mt-5 text-2xl font-semibold">${movie.tittle}</h1>
            <p class="mt-2 text-xl">₹${movie.price}</p>
        </div>
        <div>
            <a href = "/Video.html?id=${movie.id}" class="btn btn-accent ">More info</a>
        </div>
    </div>
    <div class= "mt-5 flex justify-between items-center"> 
         <kbd class = "kbd md:kbd-md w-1/2 bg-black text-green-400 mr-2">${movie.genre} </kbd>
          <kbd class = "kbd md:kbd-md w-1/2 bg-black text-cyan-400 mr-2">${new Date(movie.releaseDate).toLocaleDateString()} </kbd
     </div>
        `;
        movieGrid.appendChild(movieCard);
    });

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
    const maxpages =  Math.ceil(Totalcount / moviesPerPage);
    if(currentPage < maxpages ){
        document.getElementById('next-page').classList.remove('btn-disabled');
        currentPage++;
        currentPageText.innerText = currentPage.toString();
        displayMovies();
    }
});
const fetchMovies = async ()=>{
    try{
    const response = await fetch(`https://localhost:7029/api/Video/VideoPagination?pageNumber=${currentPage}&pageSize=${moviesPerPage}`,{
        method : 'GET',
        headers: {
            'Content-Type': 'application/json'
        },
    })
    if(!response.ok){
        const errorData = await response.json();
        console.log(errorData);
        alert('Registration Failed'+errorData.message)
        return;
    }
    
    return await response.json();

}catch(error){
console.log(error);
alert("Something Went Wrong ");
}
    
}

displayMovies();
