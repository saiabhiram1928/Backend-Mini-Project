const express = require('express');
const multer = require('multer');
const path = require('path');
const cors = require('cors');

const app = express();
app.use(cors());
app.use(express.urlencoded({ extended: true }));
app.use(express.json());

// Multer storage configuration
const storage = multer.diskStorage({
    destination: function (req, file, cb) {
        cb(null, './uploads'); // Destination folder for uploaded files
    },
    filename: function (req, file, cb) {
        const videoTitle = req.params.videoTitle;
        const index = req.files.findIndex(f => f.originalname === file.originalname) + 1;
        cb(null, `${videoTitle}_${index}.${path.extname(file.originalname).slice(1)}`);
    }
});

// File filter to accept only images
const fileFilter = (req, file, cb) => {
    if (file.mimetype.startsWith('image/')) {
        cb(null, true);
    } else {
        cb(new Error('Only images are allowed.'));
    }
};

// Multer upload middleware
const upload = multer({ storage, fileFilter });

app.post('/upload/:videoTitle', upload.array('images', 4), (req, res) => {
    // Handle uploaded files (req.files)
    console.log(req.files);
    res.send('Files uploaded successfully.');
});

// Start server
const PORT = process.env.PORT || 3000;
app.listen(PORT, () => {
    console.log(`Server is running on http://localhost:${PORT}`);
});
