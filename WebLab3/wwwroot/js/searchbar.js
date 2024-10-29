function search(event) {
    event.preventDefault();
    const searchInput = document.getElementById('searchInput').value.trim();
    if (searchInput) {
        fetch('/api/Search', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(searchInput)
        })
            .then(response => {
                if (response.ok) {
                    window.location.href = `/search.html`;
                } else {
                    alert('Failed to save search info');
                }
            })
            .catch(error => {
                console.error('Error:', error);
                alert('Search error');
            });
    }
}