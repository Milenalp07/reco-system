const API_BASE = "/api";

// 🎬 TMDB SEARCH
async function searchTmdb(query) {
    if (!query) return [];

    try {
        const res = await fetch(`/api/tmdb/search?query=${encodeURIComponent(query)}`);
        if (!res.ok) return [];
        return await res.json();
    } catch (error) {
        console.error("TMDB search error:", error);
        return [];
    }
}

// 📚 GOOGLE BOOKS SEARCH
async function searchGoogleBooks(query) {
    if (!query) return [];

    try {
        const res = await fetch(`/api/googlebooks/search?query=${encodeURIComponent(query)}`);
        if (!res.ok) return [];
        return await res.json();
    } catch (error) {
        console.error("Google Books search error:", error);
        return [];
    }
}

document.addEventListener("DOMContentLoaded", () => {
    const searchInput = document.getElementById("searchInput");
    const filterButtons = document.querySelectorAll(".filter-btn");

    const booksGrid = document.getElementById("booksGrid");
    const moviesGrid = document.getElementById("moviesGrid");
    const booksSection = document.getElementById("booksSection");
    const moviesSection = document.getElementById("moviesSection");
    const searchResults = document.getElementById("searchResults");

    if (!searchInput || !booksGrid || !moviesGrid || !booksSection || !moviesSection || !searchResults) {
        console.warn("Search elements not found in the page.");
        return;
    }

    let activeFilter = "all";
    let allBooks = [];
    let debounceTimer;

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

    function hideSearchResults() {
        searchResults.classList.add("hidden");
        booksGrid.innerHTML = "";
        moviesGrid.innerHTML = "";
    }

    function showSearchResults() {
        searchResults.classList.remove("hidden");
    }

    async function applyFilters() {
        const term = (searchInput.value || "").toLowerCase().trim();

        if (!term) {
            hideSearchResults();
            return;
        }

        showSearchResults();

        const localBooks = allBooks.filter(book => {
            const title = (book.title || "").toLowerCase();
            const author = (book.author || "").toLowerCase();
            const genre = (book.genre || "").toLowerCase();
            const year = String(book.yearPublished || book.year || "").toLowerCase();

            return (
                title.includes(term) ||
                author.includes(term) ||
                genre.includes(term) ||
                year.includes(term)
            );
        });

        let externalBooks = [];
        let filteredMovies = [];

        if (activeFilter === "all" || activeFilter === "books") {
            externalBooks = await searchGoogleBooks(term);
        }

        if (activeFilter === "all" || activeFilter === "movies") {
            filteredMovies = await searchTmdb(term);
        }

        const mergedBooks = [...localBooks, ...externalBooks];

        const filteredBooks = mergedBooks.filter((book, index, self) =>
            index === self.findIndex(b =>
                (b.title || "").toLowerCase() === (book.title || "").toLowerCase() &&
                (b.author || "").toLowerCase() === (book.author || "").toLowerCase()
            )
        );

        renderBooks(filteredBooks);
        renderMovies(filteredMovies);
        updateSectionsVisibility(filteredBooks, filteredMovies);
    }

    function updateSectionsVisibility(books, movies) {
        if (activeFilter === "books") {
            booksSection.style.display = "block";
            moviesSection.style.display = "none";
            return;
        }

        if (activeFilter === "movies") {
            booksSection.style.display = "none";
            moviesSection.style.display = "block";
            return;
        }

        booksSection.style.display = books.length ? "block" : "none";
        moviesSection.style.display = movies.length ? "block" : "none";

        if (!books.length && !movies.length) {
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

        booksGrid.innerHTML = books.map(book => {
            const isLocalBook = Number.isInteger(book.id);

            const content = `
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
            `;

            return isLocalBook
                ? `<a href="/Books/Details/${book.id}" class="media-card block">${content}</a>`
                : `<div class="media-card">${content}</div>`;
        }).join("");
    }

    function renderMovies(movies) {
        moviesGrid.innerHTML = "";

        if (!movies.length) {
            moviesGrid.innerHTML = `<p class="text-white/60 col-span-full">No movies found.</p>`;
            return;
        }

        moviesGrid.innerHTML = movies.map(movie => `
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
        `).join("");
    }

    filterButtons.forEach(button => {
        button.addEventListener("click", () => {
            filterButtons.forEach(btn => btn.classList.remove("active"));
            button.classList.add("active");

            activeFilter = button.dataset.filter;
            applyFilters();
        });
    });

    searchInput.addEventListener("input", () => {
        clearTimeout(debounceTimer);
        debounceTimer = setTimeout(() => {
            applyFilters();
        }, 250);
    });

    document.addEventListener("click", (e) => {
        const clickedInsideSearch =
            e.target.closest("#searchInput") ||
            e.target.closest("#searchResults") ||
            e.target.closest(".filter-btn");

        if (!clickedInsideSearch) {
            hideSearchResults();
        }
    });
});