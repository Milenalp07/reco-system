const booksUrl = "http://localhost:5177/api/books";
const moviesUrl = "http://localhost:5177/api/movies";

const cardsContainer = document.getElementById("cardsContainer");
const searchInput = document.getElementById("searchInput");
const navButtons = document.querySelectorAll(".nav-item");
const sectionTitle = document.getElementById("sectionTitle");
const resultsCount = document.getElementById("resultsCount");

let allItems = [];
let currentFilter = "all";

async function fetchData() {
  try {
    const [booksResponse, moviesResponse] = await Promise.all([
      fetch(booksUrl),
      fetch(moviesUrl)
    ]);

    if (!booksResponse.ok || !moviesResponse.ok) {
      throw new Error("Failed to fetch API data");
    }

    const books = await booksResponse.json();
    const movies = await moviesResponse.json();

    const formattedBooks = books.map(book => ({
      ...book,
      type: "book"
    }));

    const formattedMovies = movies.map(movie => ({
      ...movie,
      type: "movie"
    }));

    allItems = [...formattedBooks, ...formattedMovies];
    renderItems();
  } catch (error) {
    console.error(error);
    cardsContainer.innerHTML = `
      <div class="empty-message">
        Could not load data from the API.
      </div>
    `;
  }
}

function getFilteredItems() {
  const searchTerm = searchInput.value.toLowerCase().trim();

  return allItems.filter(item => {
    const matchesType =
      currentFilter === "all" ||
      (currentFilter === "books" && item.type === "book") ||
      (currentFilter === "movies" && item.type === "movie");

    const title = (item.title || "").toLowerCase();
    const genre = (item.genre || "").toLowerCase();
    const author = (item.author || "").toLowerCase();
    const year = String(item.year || "").toLowerCase();

    const matchesSearch =
      title.includes(searchTerm) ||
      genre.includes(searchTerm) ||
      author.includes(searchTerm) ||
      year.includes(searchTerm);

    return matchesType && matchesSearch;
  });
}

function getTypeLabel(type) {
  return type === "book" ? "BOOK" : "MOVIE";
}

function getIcon(type) {
  return type === "book" ? "📚" : "🎬";
}

function updateSectionTitle() {
  if (currentFilter === "books") {
    sectionTitle.textContent = "Books";
  } else if (currentFilter === "movies") {
    sectionTitle.textContent = "Movies";
  } else {
    sectionTitle.textContent = "All Items";
  }
}

function createCard(item) {
  return `
    <article class="card">
      <div class="card-cover">
        <div class="card-icon">${getIcon(item.type)}</div>
      </div>

      <div class="card-body">
        <div class="card-topline">
          <span class="card-type">${getTypeLabel(item.type)}</span>
          <span class="card-id">#${item.id ?? "-"}</span>
        </div>

        <h3 class="card-title">${item.title || "Untitled"}</h3>

        <div class="card-meta">
          ${item.author ? `<p class="meta-line"><strong>Author:</strong> ${item.author}</p>` : ""}
          ${item.genre ? `<p class="meta-line"><strong>Genre:</strong> ${item.genre}</p>` : ""}
          ${item.year ? `<p class="meta-line"><strong>Year:</strong> ${item.year}</p>` : ""}
        </div>
      </div>
    </article>
  `;
}

function renderItems() {
  const filteredItems = getFilteredItems();
  updateSectionTitle();

  resultsCount.textContent = `${filteredItems.length} item${filteredItems.length !== 1 ? "s" : ""}`;

  if (filteredItems.length === 0) {
    cardsContainer.innerHTML = `
      <div class="empty-message">
        No items found for this search.
      </div>
    `;
    return;
  }

  cardsContainer.innerHTML = filteredItems.map(createCard).join("");
}

navButtons.forEach(button => {
  button.addEventListener("click", () => {
    navButtons.forEach(btn => btn.classList.remove("active"));
    button.classList.add("active");
    currentFilter = button.dataset.type;
    renderItems();
  });
});

searchInput.addEventListener("input", renderItems);

fetchData();