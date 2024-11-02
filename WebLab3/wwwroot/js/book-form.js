$(document).ready(function () {
    document.getElementById('bookForm').addEventListener('submit', function (event) {
        event.preventDefault();
        const title = document.getElementById('title').value;
        const author = document.getElementById('author').value;
        const copies = document.getElementById('copies').value;
        const period = document.getElementById('period').value;
        const bookData = {
            title: title,
            author: author,
            copies: parseInt(copies, 10),
            period: period
        };

        fetch('/api/Books', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(bookData)
        })
            .then(response => {
                if (response.ok) {
                    updateBookData();
                    document.getElementById('bookForm').reset();
                    tryToUpdateChart();
                } else {
                    alert('Failed to save book');
                }
            })
            .catch(error => {
                console.error('Error:', error);
                alert('Connection failure.');
            });
    });
    updateBookData();
});