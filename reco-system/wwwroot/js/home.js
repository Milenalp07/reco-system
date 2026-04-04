const API_BASE = "/api";

document.addEventListener("DOMContentLoaded", () => {
    const searchInput = document.getElementById("searchInput");
    const filterButtons = document.querySelectorAll(".filter-btn");
    const searchResults = document.getElementById("searchResults");
    const booksGrid = document.getElementById("booksGrid");
    const moviesGrid = document.getElementById("moviesGrid");
    const booksSection = document.getElementById("booksSection");
    const moviesSection = document.getElementById("moviesSection");

    let activeFilter = "all";
    let allBooks = [];
    let allMovies = [];

    loadAll();

    async function loadAll() {
        try {
            const [booksResponse, moviesResponse] = await Promise.all([
                fetch(`${API_BASE}/books`),
                fetch(`${API_BASE}/movies`)
            ]);

            if (!booksResponse.ok || !moviesResponse.ok) {
                throw new Error("Failed to load API data.");
            }

            allBooks = await booksResponse.json();
            allMovies = await moviesResponse.json();

            applyFilters();
        } catch (error) {
            console.error("Error loading data:", error);

            if (booksGrid) {
                booksGrid.innerHTML = `<p class="text-white/60 col-span-full">Could not load books.</p>`;
            }

            if (moviesGrid) {
                moviesGrid.innerHTML = `<p class="text-white/60 col-span-full">Could not load movies.</p>`;
            }

            if (term.length === 0) {
                searchResults.classList.add("hidden");
                return;
            } else {
                searchResults.classList.remove("hidden");
            }
        }
    }

    function applyFilters() {
        const term = (searchInput?.value || "").toLowerCase().trim();

        const filteredBooks = allBooks.filter(book => {
            const title = (book.title || "").toLowerCase();
            const author = (book.author || "").toLowerCase();
            const genre = (book.genre || "").toLowerCase();
            return title.includes(term) || author.includes(term) || genre.includes(term);
        });

        const filteredMovies = allMovies.filter(movie => {
            const title = (movie.title || "").toLowerCase();
            const genre = (movie.genre || "").toLowerCase();
            return title.includes(term) || genre.includes(term);
        });

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
                        <img src="${book.imageUrl || 'https://via.placeholder.com/300x450?text=No+Image'}" alt="${book.title || 'Book'}" />
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
                <div class="movie-card">
                    <img src="${movie.imageUrl || 'https://via.placeholder.com/600x400?text=No+Image'}" alt="${movie.title || 'Movie'}" />
                    <div class="movie-card-overlay">
                        <span class="rating-inline">${movie.rating ?? "N/A"}</span>
                        <h3 class="movie-title-small">${movie.title || "Untitled"}</h3>
                        <p class="movie-description">${movie.genre || "No genre"}</p>
                    </div>
                </div>
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