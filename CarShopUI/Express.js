const express = require('express');
const path = require('path');
const app = express();

// .glb dosyalarının MIME tipiyle doğru şekilde sunulması
app.use('/models', express.static(path.join(__dirname, 'models'), {
  setHeaders: (res, filePath) => {
    if (filePath.endsWith('.glb')) {
      res.setHeader('Content-Type', 'model/gltf-binary');
    }
  }
}));

// Diğer statik dosyalar için
app.use(express.static(path.join(__dirname, 'public')));

// Sunucuyu başlat
app.listen(5000, () => {
  console.log('Server running at http://localhost:5000');
});