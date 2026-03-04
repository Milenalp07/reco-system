## 1. Objective
The objective of this test plan is to verify that the main features of the Book/Movie Recommendation System work correctly and meet the project requirements.

## 2. Scope (What will be tested)
The QA testing will focus on the core user functionalities:
- User login/register (if available)
- Search for books/movies
- View item details (book/movie)
- Display recommendations
- Favorites / rating features (if implemented)

Out of scope:
- Advanced performance testing
- Security testing beyond basic validation

## 3. Test Approach
Testing will be mainly manual testing based on predefined test cases.
- Manual testing: execute test cases and record results (Pass/Fail)
- Regression testing: re-test key features after fixes
- Evidence: screenshots of results and GitHub Issues links

If required, basic automated tests may be added (example: unit tests), and shown in the CI pipeline.

## 4. Tools
- GitHub Issues: bug reporting and tracking
- GitHub Projects/Board (optional): track bug status (To do / In progress / Done)
- GitHub Actions (CI): run build and tests automatically

## 5. Test Environment
Tests will be executed in:
- Local development environment (VS Code)
- Docker environment (if available)
- Supported browser: Chrome (latest)

## 6. Entry and Exit Criteria
Entry criteria:
- Core features are implemented and deployed locally or via Docker
- Test cases are available

Exit criteria:
- All planned test cases executed
- Major bugs are reported and addressed or documented

## 7. Deliverables
- Test case document
- Test results (Pass/Fail)
- Bug reports (GitHub Issues)
- Evidence of testing in CI pipeline (GitHub Actions)