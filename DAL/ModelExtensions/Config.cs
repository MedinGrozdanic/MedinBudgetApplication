//using DAL.Model;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Metadata.Builders;

//namespace DAL.ModelExtensions
//{
//    public class ExpenseEntityConfig : IEntityTypeConfiguration<Expense>
//    {
//        public void Configure( EntityTypeBuilder<Expense> builder )
//        {
//            builder
//                .HasKey( k => k.ExpenseId )
//                ;
//        }
//    }

//    public class UserEntityConfig : IEntityTypeConfiguration<User>
//    {
//        public void Configure( EntityTypeBuilder<User> builder )
//        {
//            builder
//                .HasIndex( u => u.Username )
//                .IsUnique()
//                ;

//            builder
//                .HasIndex( e => e.Email )
//                .IsUnique()
//                ;
//        }
//    }

//    public class CategoryEntityConfig : IEntityTypeConfiguration<Category>
//    {
//        public void Configure( EntityTypeBuilder<Category> builder )
//        {
//            builder
//                .HasKey( k => k.CategoryId )
//                ;

//            builder
//                .HasIndex( i => i.CategoryName ).IsUnique();
//            ;
//        }
//    }
//    //public class BudgetCategoryExpenseEntityConfig : IEntityTypeConfiguration<BudgetCategoryExpense>
//    //{
//    //    public void Configure( EntityTypeBuilder<BudgetCategoryExpense> builder )
//    //    {
//    //        builder
//    //            .HasKey( pk => new {  pk.BudgetId , pk.CategoryId , pk.ExpenseId  } )
//    //        ;

//    //    }
//    //}
//}

