document.addEventListener('DOMContentLoaded', () => {
    const params = new URLSearchParams(window.location.search);
    const videoId = params.get('id');
    fetchVideoDetails(videoId);
});

const fetchVideoDetails = async(id) => {
    try {
        const response = await fetch(`https://localhost:7029/api/Video/GetVideoById?id=${id}`);
        if (!response.ok) {
            throw new Error('Network response was not ok');
        }
        const video = await response.json();
        console.log(video);
        updateVideoDetails(video);
    } catch (error) {
        console.error('Fetch error:', error);
    }
}

const updateVideoDetails = (video) => {
    document.getElementById('video-image').src = `./Rendering Server/uploads/${video.tittle}_0.png`; // Update with actual image URL if available
    document.getElementById('video-title').innerText = `${video.tittle}, ${video.genre} Movie, by ${video.director}`;
    document.getElementById('video-price').innerText = `$${video.price}`;
    document.getElementById('video-release-date').innerText = `Release Date: ${new Date(video.releaseDate).toLocaleDateString()}`;
    document.getElementById('video-description').innerText = video.description;

    const quantitySelect = document.getElementById('video-quantity');
    for (let i = 1; i <= video.stock; i++) {
        const option = document.createElement('option');
        option.value = i;
        option.innerText = i;
        quantitySelect.appendChild(option);
    }

    const additionalImagesContainer = document.getElementById('additional-images');
    for (let i = 1; i <= 2; i++) {
        const img = document.createElement('img');
        img.src = `./Rendering Server/uploads/${video.tittle}_${i}.png`;
        img.alt = `${video.tittle}_${i}`;
        img.className = "w-full h-auto cursor-pointer";
        img.addEventListener('click', () => {
            document.getElementById('video-image').src = img.src;
        });
        additionalImagesContainer.appendChild(img);
    }
}

document.getElementById("add-to-cart").addEventListener("click",()=>{
    let quantity = document.getElementById('video-quantity').value;
    if (quantity != "Qty") {
        const params = new URLSearchParams(window.location.search);
        let videoId = params.get('id');
        console.log(typeof videoId ,typeof quantity);
        videoId = parseInt(videoId)
        quantity = parseInt(quantity)
        console.log(typeof videoId ,typeof quantity);
        addToCart(videoId, quantity);
    } else {
        alert('Please select a quantity');
    }
})

const addToCart = async(videoId, quantity) => {
    const addToCartButton = document.getElementById('add-to-cart');
    // const loader = document.getElementById('loader');

    try {
        // loader.classList.remove('hidden');
        const response = await fetchWithAuth(`https://localhost:7029/api/Cart/AddToCart?videoId=${videoId}&qty=${quantity}`, {
            method: 'POST'
        });
        if (!response.ok) {
            console.log(response);
        }
        console.log(response);
        showSuccessGlow(addToCartButton);
        location.reload();
    } catch (error) {
        console.error('Add to cart error:', error);
        alert("Something Went Wrong , Please Try Again Later")
    } finally {
        // loader.classList.add('hidden');
    }
}

const showSuccessGlow = (button) => {
    console.log(button);
    button.classList.add('bg-gold');
    button.classList.add('text-black');
    button.classList.add('animate-rubberband');
    button.innerText = "Added To Cart"
    setTimeout(() => {
        button.classList.remove('bg-gold');
        button.classList.remove('text-black');
        button.classList.remove('animate-rubberband')
        button.innerText = "Add To Cart"
    }, 1000);
}

const fetchWithAuth = async (url, options = {}) => {
    const token = getCookie('token');
    if (!options.headers) {
        options.headers = {};
    }
    if (token) {
        options.headers['Authorization'] = `Bearer ${token}`;
    }

    const response = await fetch(url, options);
    console.log(response);
    if(!response.ok) throw new Error(response);

    if (response.status === 401) {
        window.location.href = '/Login.html'; 
    }

    return response;
};

const getCookie = (name) => {
    let nameEQ = name + "=";
    let ca = document.cookie.split(';');
    for (let i = 0; i < ca.length; i++) {
        let c = ca[i];
        while (c.charAt(0) == ' ') c = c.substring(1, c.length);
        if (c.indexOf(nameEQ) == 0) return c.substring(nameEQ.length, c.length);
    }
    return null;
};
