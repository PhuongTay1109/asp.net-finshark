document.getElementById('uploadForm').addEventListener('submit', async (event) => {
    event.preventDefault();
    
    const formData = new FormData();
    const imageFile = document.getElementById('image').files[0];
    const name = document.getElementById('name').value;

    formData.append('image', imageFile);

    try {
        const response = await fetch(`http://localhost:5201/upload?name=${encodeURIComponent(name)}`, {
            method: 'POST',
            body: formData
        });

        if (!response.ok) {
            throw new Error('Network response was not ok' + response.statusText);
        }

        const result = await response.json();
        alert("Image uploaded successfully");
        document.getElementById('uploadForm').reset();
    } catch (error) {
        alert("Upload failed");
    }
});
