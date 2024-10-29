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

function updateBookTable(books, tableName) {
    const tableBody = document.getElementById(tableName).getElementsByTagName('tbody')[0];
    tableBody.innerHTML = '';

    if (books.length > 0) {
        books.forEach(book => {
            const row = document.createElement('tr');

            const titleCell = document.createElement('td');
            titleCell.textContent = book.title;
            row.appendChild(titleCell);

            const authorCell = document.createElement('td');
            authorCell.textContent = book.author;
            row.appendChild(authorCell);

            const copiesCell = document.createElement('td');
            copiesCell.textContent = book.copies;
            row.appendChild(copiesCell);

            const periodCell = document.createElement('td');
            periodCell.textContent = book.period;
            row.appendChild(periodCell);

            tableBody.appendChild(row);
        });
    } else {
        const row = document.createElement('tr');
        const cell = document.createElement('td');
        cell.colSpan = 3;
        cell.textContent = "No books available";
        row.appendChild(cell);
        tableBody.appendChild(row);
    }
}