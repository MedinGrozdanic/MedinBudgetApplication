using DAL;
using DAL.Model;
using Microsoft.EntityFrameworkCore;
using Service.Services.ServiceInterfaces;

namespace Service.Services
{
    public class CategoryService : ICategoryService
    {


        public List<Category> ListCategories()
        {
            using (var context = new BudgetContext())
            {
               var categories = new List<Category>();
               
                try
                {
                    //var cat = context.Categories;
                    categories.AddRange(context.Categories.ToList());
                }
                catch (NullReferenceException)
                {
                }

                return categories;
            }

        }



        //public List<Category> GetCategoriesViaExpenseId(int expenseId)
        //{
        //    using (var context = new BudgetContext())
        //    {
        //        List<Category> categories;
        //        try
        //        {
        //            categories = context.Categories.Where(c => c.Expenses.Any(u => u.ExpenseId == expenseId)).OrderBy(e => e.CategoryId).Reverse().ToList();
        //        }
        //        catch (NullReferenceException)
        //        {
        //            categories = new List<Category>();
        //        }

        //        return categories;
        //    }
        //}

        //        public Category? GetCategoryById( int? categoryId )
        //        {
        //            using (var context = new BudgetContext())
        //            {
        //                return context.Categories.Find( categoryId ) ?? throw new Exception( "Categories not found" );
        //            }
        //        }

        //        public bool CheckIfCategoryExists( string categoryName , int userId )
        //        {
        //            using (var context = new BudgetContext())
        //            {
        //                return ListCategories( userId ).Exists( c => c.CategoryName == categoryName ) || context.Categories.Any( c => c.CategoryName == categoryName );
        //            }
        //        }

        //        public Category CreateCategory( string categoryName , int userId )
        //        {
        //            using (var context = new BudgetContext())
        //            {
        //                // check if category exists in db
        //                //var category = context.Categories.Include(u => u.Users).FirstOrDefault( c => c.CategoryName == categoryName );
        //                var category = context.Categories.FirstOrDefault( c => c.CategoryName == categoryName );
        //                var user = context.Users.Find(userId); // anropa UserService? // attach? context.Update(category)?
        //                try
        //                {
        //                    if (category == null)
        //                    {
        //                        category = new Category()
        //                        {
        //                            CategoryName = categoryName ,
        //                            //Users = new List<User>() { user } ,
        //                        };
        //                        context.Categories.Add( category );
        //                    }
        //                    // check if category is connected to user, if not add user to category.Users
        //                    //else if (!category.Users.Contains( user )) { category.Users.Add( user ); }

        //                    context.SaveChanges();
        //                }
        //                catch (Exception)
        //                {
        //                    throw;
        //                }

        //                return category; // DTO in Service.DTO ?
        //            }
        //        }

        //        public void DeleteCategory( int categoryId , int userId )
        //        {
        //            using (var context = new BudgetContext())
        //            {
        //                //var category = context.Categories.Include(u => u.Users).SingleOrDefault(c => c.CategoryId == categoryId);
        //                //var user = context.Users.Find( userId );
        //                //if (category.Users.Contains( user ))
        //                //{
        //                //    category.Users.Remove( user );

        //                //    // check if if any user is connected to the category, if not --> delete from database
        //                //    // utveckla ... default on delete osv ...
        //                //    // just nu försvinner alla expenses när sista användare raderar
        //                //    if (category.Users.Count() == 0)
        //                //    {
        //                //        context.Categories.Remove( category );
        //                //    }
        //                //    context.SaveChanges();
        //                //}
        //                //else
        //                //{
        //                //    throw new Exception( "Unable to delete category" ); // fixa ifelse null osv ...
        //                //}
        //            }
        //        }

        //        public void DeleteCategory( string categoryName , int userId ) // categoryId istället? userId istället?
        //        {
        //            using (var context = new BudgetContext())
        //            {
        //                //var category = context.Categories.Include(u => u.Users).SingleOrDefault( c => c.CategoryName == categoryName );
        //                //var user = context.Users.Find( userId );
        //                //if (category.Users.Contains( user ))
        //                //{
        //                //    category.Users.Remove( user );

        //                //    // check if if any user is connected to the category, if not --> delete from database
        //                //    // utveckla ... default on delete osv ...
        //                //    // just nu försvinner alla expenses när sista användare raderar
        //                //    if (category.Users.Count() == 0)
        //                //    {
        //                //        context.Categories.Remove( category );
        //                //    }
        //                //    context.SaveChanges();
        //                //}
        //                //else
        //                //{
        //                //    throw new Exception( "Unable to delete category" );
        //                //}
        //            }

        //            //using (var context = new BudgetContext())
        //            //{
        //            //    //if ( category.Users.Contains( user ) )
        //            //    //{
        //            //    //}

        //            //    var category = ListCategories(userId).FirstOrDefault(t => t.CategoryName == categoryName) ?? throw new Exception("Category not found");

        //            //    var user = context.Users.Find(userId) ?? throw new Exception( "Users not found" );
        //            //    user.Categories.RemoveAll( Category => Category.CategoryId == category.CategoryId );

        //            //    List<Expense> expenses;
        //            //    // get all expenses where category is used and Users contains userId
        //            //    try
        //            //    {
        //            //        expenses = context.Expenses.Where( e => e.Categories.Contains( category ) && e.Users == user ).ToList();
        //            //    }
        //            //    catch (NullReferenceException)
        //            //    {
        //            //        expenses = new List<Expense>();
        //            //    }

        //            //    //var expense = context.Expenses.Include(c => c.Categories).Where(u => u.UserId == userId).ToList();

        //            //    //var defaultCategory = context.Categories.Find(1); // hårdkoda?
        //            //    ////var expenses = context.Expenses.Include( c => c.Categories ).Where( u => u.UserId == userId && u.Categories.Contains(category)).ToList();
        //            //    //foreach (var ex in expenses)
        //            //    //{
        //            //    //    ex.Categories.RemoveAll( Categories => Categories.CategoryId == category.CategoryId ); // varför lirar inte .Remove?

        //            //    //    if (ex.Categories.Count == 0)
        //            //    //    {
        //            //    //        ex.Categories.Add( defaultCategory ); // add default if no category remains after delete
        //            //    //    }
        //            //    //}
        //            //    //user.Categories.RemoveAll( Categories => Categories.CategoryId == category.CategoryId ); // Remove reference to category from user

        //            //    ////remova från db om tomt?
        //            //    ////if (!context.Expenses.Any( u => u.Categories.Contains( category ) ))
        //            //    ////{
        //            //    ////    context.Categories.Remove( category );
        //            //    ////    return;
        //            //    ////}

        //            //    try
        //            //    {
        //            //        context.SaveChanges();
        //            //    }
        //            //    catch (Exception ex)
        //            //    {
        //            //        throw ex;
        //            //    }
        //            //}
        //        }
        //    }
        //}

        //// remove all expenses where category is used and Users contains userId

        ////var expenses = context.Expenses.Include( c => c.Categories ).Where(c => c.Categories.Contains(category));
        ////using (var context2 = new BudgetContext())
        ////{
        ////    foreach (var ex in expenses)
        ////    {
        ////        ex.Categories.Add( context2.Categories.Find( 1 ) );
        ////        context2.SaveChanges();
        ////    }
        ////}
        //////context.Update( category );
    }
}
    
