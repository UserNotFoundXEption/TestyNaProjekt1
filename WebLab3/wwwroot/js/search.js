$(document).ready(function () {
    document.getElementById('searchForm').addEventListener('submit', search);
    updateSearchResults();
});

async function updateSearchResults() {
    const tableBody = document.getElementById('bookTableSearch').getElementsByTagName('tbody')[0];
    tableBody.innerHTML = '';

    const books = await getSearchResults();
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

async function getSearchResults() {
    try {
        const response = await fetch('/api/Search', { method: 'GET' });
        if (!response.ok) {
            throw new Error('Failed to load search info');
        }
        const searchInfo = await response.json();
        return searchInfo;
    } catch (error) {
        console.error('Error:', error);
        alert('Failed to load search info');
        return [];
    }
}