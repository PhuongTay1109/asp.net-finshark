document.getElementById('uploadForm').addEventListener('submit', async (event) => {
    event.preventDefault();
    
    const formData = new FormData();
    const imageFile = document.getElementById('image').files[0];
    const name = document.getElementById('name').value;

    if (!imageFile) {
        alert("Please select an image.");
        return;
    }

    if (!name) {
        alert("Please enter a name for the image.");
        return;
    }

    formData.append('image', imageFile);

    try {
        const response = await fetch(`http://localhost:5201/upload?name=${encodeURIComponent(name)}`, {
            method: 'POST',
            body: formData
        });

        if (!response.ok) {
            throw new Error('Network response was not ok' + response.statusText);
        }

        const contentType = response.headers.get('content-type');
        if (contentType && contentType.includes('application/json')) {
            const result = await response.json();
            alert(`Image uploaded successfully: ID = ${result.Id}, Name = ${result.Name}`);
        } else {
            alert('Unexpected response format');
        }

        // Reset the form fields
        document.getElementById('uploadForm').reset();
    } catch (error) {
        alert(`Upload failed: ${error.message}`);
    }
});
