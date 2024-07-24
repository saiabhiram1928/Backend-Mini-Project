document.getElementById('addMovieForm').addEventListener('submit', async (e) => {
    e.preventDefault();

    const form = e.target;
    const formData = new FormData(form);

    // Extract and validate non-file fields
    const tittle = formData.get('tittle');
    const description = formData.get('description');
    const genre = formData.get('genre');
    const director = formData.get('director');
    const releaseDate = formData.get('releaseDate');
    const price = parseFloat(formData.get('price'));
    const stock = parseInt(formData.get('stock'));

    if (isNaN(price) || price < 0) {
        alert('Price must be a non-negative number.');
        return;
    }

    if (isNaN(stock) || stock < 0) {
        alert('Stock must be a non-negative integer.');
        return;
    }

    const movieData = {
        tittle,
        description,
        genre,
        director,
        releaseDate: new Date(releaseDate).toISOString(),
        price,
        stock
    };

    // Handle file uploads
    if (selectedFiles.length < 1 || selectedFiles.length > 4) {
        alert('Please upload between 1 and 4 images.');
        return;
    }
    console.log(movieData);
    try {
        // Send images to Node.js server
        const imageFormData = new FormData();
        selectedFiles.forEach((file, index) => {
            imageFormData.append('images', file, `${movieData.tittle}_${index + 1}.${file.name.split('.').pop()}`);
        });

        const imageResponse = await fetch(`http://localhost:3000/upload/${movieData.tittle}`, {
            method: 'POST',
            body: imageFormData
        });

        if (!imageResponse.ok) {
            throw new Error('Failed to upload images.');
        }

        // Send movie data to backend to get the video ID
        const movieResponse = await fetchWithAuth('https://localhost:7029/api/Video/AddVideo', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(movieData)
        });

        if (!movieResponse.ok) {
            const data = await movieResponse.json();
            throw new Error(`Failed to add movie: ${data.message}`);
        }

        const movie = await movieResponse.json();

        alert('Movie added successfully!');
        // form.reset(); // Reset the form after successful submission
        // selectedFiles.length = 0; // Clear selected files array
        updateFileNames(); // Update UI to reflect cleared files
    } catch (error) {
        console.error('Error:', error);
        alert('An error occurred.');
    }
});

const fetchWithAuth = async (url, options = {}) => {
    const token = document.cookie.split(';').find(cookie => cookie.trim().startsWith('token='));
    if (!options.headers) {
        options.headers = {};
    }
    if (token) {
        options.headers['Authorization'] = `Bearer ${token.split('=')[1]}`;
    }

    const response = await fetch(url, options);

    if (response.status === 401) {
        window.location.href = '/Login.html';
    }

    return response;
};

const imageNamesDiv = document.getElementById('imageNames');
const selectedFiles = [];

document.getElementById('images').addEventListener('change', (e) => {
    Array.from(e.target.files).forEach((file) => {
        if (selectedFiles.length < 4) {
            // Check file extension to allow only PNG format
            if (!file.name.toLowerCase().endsWith('.png')) {
                alert('Please upload only PNG images.');
                return;
            }

            selectedFiles.push(file);
            const fileIndex = selectedFiles.length - 1;
            const fileItemElement = document.createElement('div');
            fileItemElement.className = 'file-item';
            fileItemElement.innerHTML = `<p>Image ${fileIndex + 1}: ${file.name}</p><button data-index="${fileIndex}">X</button>`;
            imageNamesDiv.appendChild(fileItemElement);

            // Add event listener for the remove button
            fileItemElement.querySelector('button').addEventListener('click', (e) => {
                const index = e.target.getAttribute('data-index');
                selectedFiles.splice(index, 1);
                fileItemElement.remove();
                updateFileNames();
            });
        }
    });

    if (selectedFiles.length > 4) {
        selectedFiles.splice(4);
        alert('Only 4 images can be uploaded.');
    }

    e.target.value = ''; // Clear input to allow re-selection of the same files if needed
});


const updateFileNames = () => {
    imageNamesDiv.innerHTML = '';
    selectedFiles.forEach((file, index) => {
        const fileItemElement = document.createElement('div');
        fileItemElement.className = 'file-item';
        fileItemElement.innerHTML = `<p>Image ${index + 1}: ${file.name}</p><button data-index="${index}">X</button>`;
        imageNamesDiv.appendChild(fileItemElement);

        // Add event listener for the remove button
        fileItemElement.querySelector('button').addEventListener('click', (e) => {
            const index = e.target.getAttribute('data-index');
            selectedFiles.splice(index, 1);
            fileItemElement.remove();
            updateFileNames();
        });
    });
};