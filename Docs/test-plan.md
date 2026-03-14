## \# Test Plan — Book/Movie Recommendation System

## 

## \## 1. Objective

## The objective of this test plan is to verify that the main features of the Book/Movie Recommendation System work correctly and meet the project requirements.

## 

## \## 2. Scope (What will be tested)

## The QA testing will focus on the core user functionalities:

## \- User login/register (if available)

## \- Search for books/movies

## \- View item details (book/movie)

## \- Display recommendations

## \- Favorites / rating features (if implemented)

## 

## Out of scope:

## \- Advanced performance testing

## \- Security testing beyond basic validation

## 

## \## 3. Test Approach

## Testing is divided into two types:

## 

## \### 3.1 Manual Testing

## \- Execute test cases and record results (Pass/Fail)

## \- Regression testing: re-test key features after fixes

## \- Evidence: screenshots of results and GitHub Issues links

## 

## \### 3.2 Automated Testing (CI/CD Pipeline)

## Automated unit tests were implemented using \*\*xUnit\*\* and run automatically via \*\*GitHub Actions\*\* every time new code is pushed to the repository.

## 

## The automated tests cover:

## \- `GetMovieRecommendations\_NoFilters\_ReturnsTop5ByRating` — verifies top 5 movies are returned ordered by rating

## \- `GetMovieRecommendations\_ByGenre\_ReturnsOnlyThatGenre` — verifies genre filter works correctly

## \- `GetMovieRecommendations\_ByMinRating\_ExcludesLowRated` — verifies low rated movies are excluded

## \- `GetMovieRecommendations\_UnknownGenre\_ReturnsEmptyList` — verifies empty result for unknown genre

## \- `GetMovieRecommendations\_ResultsAreOrderedByRatingDescending` — verifies correct ordering

## \- `GetBookRecommendations\_NoFilters\_ReturnsTop5ByRating` — verifies top 5 books are returned ordered by rating

## \- `GetBookRecommendations\_ByGenre\_ReturnsOnlyThatGenre` — verifies genre filter works correctly

## \- `GetBookRecommendations\_ByMinRating\_ExcludesLowRated` — verifies low rated books are excluded

## \- `GetBookRecommendations\_UnknownGenre\_ReturnsEmptyList` — verifies empty result for unknown genre

## \- `GetBookRecommendations\_ResultsAreOrderedByRatingDescending` — verifies correct ordering

## 

## \## 4. Tools

## \- \*\*xUnit\*\* — automated unit testing framework (.NET)

## \- \*\*GitHub Actions\*\* — CI/CD pipeline that runs tests automatically on every push

## \- \*\*GitHub Issues\*\* — bug reporting and tracking

## \- \*\*Entity Framework InMemory\*\* — in-memory database used during automated tests

## 

## \## 5. Test Environment

## Tests are executed in:

## \- Local development environment (VS Code)

## \- Docker environment (if available)

## \- GitHub Actions (Ubuntu, automated)

## \- Supported browser: Chrome (latest)

## 

## \## 6. Entry and Exit Criteria

## Entry criteria:

## \- Core features are implemented and deployed locally or via Docker

## \- Test cases are available

## 

## Exit criteria:

## \- All planned test cases executed

## \- All automated tests passing in GitHub Actions (Status: Success)

## \- Major bugs are reported and addressed or documented

## 

## \## 7. Deliverables

## \- Test case document (this file)

## \- Test results (Pass/Fail)

## \- Bug reports (GitHub Issues)

## \- Automated test file: `api.Tests/UnitTest1.cs`

## \- CI/CD pipeline: `.github/workflows/ci.yml`

## \- Evidence of passing tests in GitHub Actions (screenshot)

