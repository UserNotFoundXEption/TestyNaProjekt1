$(document).ready(function () {
    document.getElementById('searchForm').addEventListener('submit', search);
    updateSearchResults();
});

async function updateSearchResults() {
    const books = await getSearchResults();
    updateBookTable(books, 'bookTableSearch');
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