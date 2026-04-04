using Microsoft.AspNetCore.Mvc;
using reco_system.Controllers;
using reco_system.Models;
using Xunit;

namespace reco_system.Tests.Controllers
{
    public class BooksControllerTests
    {
        [Fact]
        public void Index_Returns_ViewResult_With_List_Of_Books()
        {
            var controller = new BooksController();

            var result = controller.Index();

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Book>>(viewResult.Model);

            Assert.NotEmpty(model);
        }

        [Fact]
        public void Details_Returns_ViewResult_When_Book_Exists()
        {
            var controller = new BooksController();

            var result = controller.Details(1);

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<Book>(viewResult.Model);

            Assert.Equal(1, model.Id);
        }

        [Fact]
        public void Details_Returns_NotFound_When_Book_Does_Not_Exist()
        {
            var controller = new BooksController();

            var result = controller.Details(999);

            Assert.IsType<NotFoundResult>(result);
        }
    }
}