const API_BASE = "/api";

// 🎬 TMDB SEARCH
async function searchTmdb(query) {
    if (!query) return [];

    const res = await fetch(`/api/tmdb/search?query=${encodeURIComponent(query)}`);
    if (!res.ok) return [];

    return await res.json();
}

// 📚 GOOGLE BOOKS SEARCH
async function searchGoogleBooks(query) {
    if (!query) return [];

    const res = await fetch(`/api/googlebooks/search?query=${encodeURIComponent(query)}`);
    if (!res.ok) return [];

    return await res.json();
}

document.addEventListener("DOMContentLoaded", () => {
    const searchInput = document.getElementById("searchInput");
    const filterButtons = document.querySelectorAll(".filter-btn");

    const booksGrid = document.getElementById("booksGrid");
    const moviesGrid = document.getElementById("moviesGrid");
    const booksSection = document.getElementById("booksSection");
    const moviesSection = document.getElementById("moviesSection");
    const searchResults = document.getElementById("searchResults");

    let activeFilter = "all";
    let allBooks = [];

    loadBooks();

    async function loadBooks() {
        try {
            const response = await fetch(`${API_BASE}/books`);

            if (!response.ok) {
                throw new Error("Failed to load books");
            }

            allBooks = await response.json();
        } catch (error) {
            console.error("Error loading books:", error);
            allBooks = [];
        }
    }

    async function applyFilters() {
        const term = (searchInput?.value || "").toLowerCase().trim();

        if (!term) {
            searchResults.classList.add("hidden");
            booksGrid.innerHTML = "";
            moviesGrid.innerHTML = "";
            return;
        }

        searchResults.classList.remove("hidden");

        const localBooks = allBooks.filter(book => {
            const title = (book.title || "").toLowerCase();
            const author = (book.author || "").toLowerCase();
            const genre = (book.genre || "").toLowerCase();

            return (
                title.includes(term) ||
                author.includes(term) ||
                genre.includes(term)
            );
        });

        const externalBooks = await searchGoogleBooks(term);
        const filteredMovies = await searchTmdb(term);

        const mergedBooks = [...localBooks, ...externalBooks];

        const filteredBooks = mergedBooks.filter((book, index, self) =>
            index === self.findIndex(b =>
                (b.title || "").toLowerCase() === (book.title || "").toLowerCase() &&
                (b.author || "").toLowerCase() === (book.author || "").toLowerCase()
            )
        );

        renderBooks(filteredBooks);
        renderMovies(filteredMovies);

        if (activeFilter === "books") {
            booksSection.style.display = "block";
            moviesSection.style.display = "none";
        } else if (activeFilter === "movies") {
            booksSection.style.display = "none";
            moviesSection.style.display = "block";
        } else {
            booksSection.style.display = "block";
            moviesSection.style.display = "block";
        }
    }

    function renderBooks(books) {
        booksGrid.innerHTML = "";

        if (!books.length) {
            booksGrid.innerHTML = `<p class="text-white/60 col-span-full">No books found.</p>`;
            return;
        }

        books.forEach(book => {
            booksGrid.innerHTML += `
                <div class="media-card">
                    <div class="media-poster">
                        <img 
                            src="${book.imageUrl || 'https://via.placeholder.com/300x450?text=No+Image'}" 
                            alt="${book.title || "Book"}"
                        />
                        <div class="media-overlay">
                            <span class="rating-badge">${book.rating ?? "N/A"}</span>
                        </div>
                    </div>
                    <h3 class="media-title">${book.title || "Untitled"}</h3>
                    <p class="media-subtitle">${book.author || "Unknown Author"}</p>
                </div>
            `;
        });
    }

    function renderMovies(movies) {
        moviesGrid.innerHTML = "";

        if (!movies.length) {
            moviesGrid.innerHTML = `<p class="text-white/60 col-span-full">No movies found.</p>`;
            return;
        }

        movies.forEach(movie => {
            moviesGrid.innerHTML += `
                <a href="/Movies/TmdbDetails/${movie.id}" class="movie-card block">
                    <img 
                        src="${movie.imageUrl || 'https://via.placeholder.com/600x400?text=No+Image'}" 
                        alt="${movie.title || "Movie"}" 
                    />
                    <div class="movie-card-overlay">
                        <span class="rating-inline">${movie.rating ?? "N/A"}</span>
                        <h3 class="movie-title-small">${movie.title || "Untitled"}</h3>
                    </div>
                </a>
            `;
        });
    }

    filterButtons.forEach(button => {
        button.addEventListener("click", () => {
            filterButtons.forEach(btn => btn.classList.remove("active"));
            button.classList.add("active");

            activeFilter = button.dataset.filter;
            applyFilters();
        });
    });

    if (searchInput) {
        searchInput.addEventListener("input", applyFilters);
    }
});