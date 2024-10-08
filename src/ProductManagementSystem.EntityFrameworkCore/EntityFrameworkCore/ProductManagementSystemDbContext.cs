using ProductManagementSystem.Currencies;
using ProductManagementSystem.Products;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.BackgroundJobs.EntityFrameworkCore;
using Volo.Abp.BlobStoring.Database.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.Modeling;
using Volo.Abp.FeatureManagement.EntityFrameworkCore;
using Volo.Abp.Identity;
using Volo.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.SettingManagement.EntityFrameworkCore;
using Volo.Abp.OpenIddict.EntityFrameworkCore;
using Volo.Abp.LanguageManagement.EntityFrameworkCore;
using Volo.Abp.TextTemplateManagement.EntityFrameworkCore;
using Volo.Saas.EntityFrameworkCore;
using Volo.Saas.Editions;
using Volo.Saas.Tenants;
using Volo.Abp.Gdpr;

namespace ProductManagementSystem.EntityFrameworkCore;

[ReplaceDbContext(typeof(IIdentityProDbContext))]
[ReplaceDbContext(typeof(ISaasDbContext))]
[ConnectionStringName("Default")]
public class ProductManagementSystemDbContext :
    AbpDbContext<ProductManagementSystemDbContext>,
    ISaasDbContext,
    IIdentityProDbContext
{
    public DbSet<Currency> Currencies { get; set; } = null!;
    public DbSet<Product> Products { get; set; } = null!;
    /* Add DbSet properties for your Aggregate Roots / Entities here. */

    #region Entities from the modules

    /* Notice: We only implemented IIdentityProDbContext and ISaasDbContext
     * and replaced them for this DbContext. This allows you to perform JOIN
     * queries for the entities of these modules over the repositories easily. You
     * typically don't need that for other modules. But, if you need, you can
     * implement the DbContext interface of the needed module and use ReplaceDbContext
     * attribute just like IIdentityProDbContext and ISaasDbContext.
     *
     * More info: Replacing a DbContext of a module ensures that the related module
     * uses this DbContext on runtime. Otherwise, it will use its own DbContext class.
     */

    // Identity
    public DbSet<IdentityUser> Users { get; set; }
    public DbSet<IdentityRole> Roles { get; set; }
    public DbSet<IdentityClaimType> ClaimTypes { get; set; }
    public DbSet<OrganizationUnit> OrganizationUnits { get; set; }
    public DbSet<IdentitySecurityLog> SecurityLogs { get; set; }
    public DbSet<IdentityLinkUser> LinkUsers { get; set; }
    public DbSet<IdentityUserDelegation> UserDelegations { get; set; }
    public DbSet<IdentitySession> Sessions { get; set; }

    // SaaS
    public DbSet<Tenant> Tenants { get; set; }
    public DbSet<Edition> Editions { get; set; }
    public DbSet<TenantConnectionString> TenantConnectionStrings { get; set; }

    #endregion

    public ProductManagementSystemDbContext(DbContextOptions<ProductManagementSystemDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        /* Include modules to your migration db context */

        builder.ConfigurePermissionManagement();
        builder.ConfigureSettingManagement();
        builder.ConfigureBackgroundJobs();
        builder.ConfigureAuditLogging();
        builder.ConfigureFeatureManagement();
        builder.ConfigureIdentityPro();
        builder.ConfigureOpenIddictPro();
        builder.ConfigureLanguageManagement();
        builder.ConfigureSaas();
        builder.ConfigureTextTemplateManagement();
        builder.ConfigureGdpr();
        builder.ConfigureBlobStoring();

        /* Configure your own tables/entities inside here */

        //builder.Entity<YourEntity>(b =>
        //{
        //    b.ToTable(ProductManagementSystemConsts.DbTablePrefix + "YourEntities", ProductManagementSystemConsts.DbSchema);
        //    b.ConfigureByConvention(); //auto configure for the base class props
        //    //...
        //});
        if (builder.IsHostDatabase())
        {

        }
        if (builder.IsHostDatabase())
        {

        }
        if (builder.IsHostDatabase())
        {
            builder.Entity<Currency>(b =>
            {
                b.ToTable(ProductManagementSystemConsts.DbTablePrefix + "Currency", ProductManagementSystemConsts.DbSchema);
                b.ConfigureByConvention();
                b.Property(x => x.Code).HasColumnName(nameof(Currency.Code)).IsRequired().HasMaxLength(CurrencyConsts.CodeMaxLength);
                b.Property(x => x.Numeric).HasColumnName(nameof(Currency.Numeric));
                b.Property(x => x.Name).HasColumnName(nameof(Currency.Name)).HasMaxLength(CurrencyConsts.NameMaxLength);
                b.Property(x => x.Symbol).HasColumnName(nameof(Currency.Symbol)).HasMaxLength(CurrencyConsts.SymbolMaxLength);
                b.Property(x => x.Country).HasColumnName(nameof(Currency.Country)).HasMaxLength(CurrencyConsts.CountryMaxLength);
                b.Property(x => x.Active).HasColumnName(nameof(Currency.Active));
                b.Property(x => x.Order).HasColumnName(nameof(Currency.Order));
            });

        }
        if (builder.IsHostDatabase())
        {
            builder.Entity<Product>(b =>
            {
                b.ToTable(ProductManagementSystemConsts.DbTablePrefix + "Product", ProductManagementSystemConsts.DbSchema);
                b.ConfigureByConvention();
                b.Property(x => x.Name).HasColumnName(nameof(Product.Name)).IsRequired().HasMaxLength(ProductConsts.NameMaxLength);
                b.Property(x => x.Code).HasColumnName(nameof(Product.Code)).IsRequired().HasMaxLength(ProductConsts.CodeMaxLength);
                b.Property(x => x.Price).HasColumnName(nameof(Product.Price));
                b.Property(x => x.Quantity).HasColumnName(nameof(Product.Quantity));
                b.HasOne<Currency>().WithMany().IsRequired().HasForeignKey(x => x.CurrencyId).OnDelete(DeleteBehavior.NoAction);
            });

        }
    }
}