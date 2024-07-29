using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrderingSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePaymentType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:entity_state", "detached,unchanged,deleted,modified,added")
                .Annotation("Npgsql:Enum:menu_status", "available,not_active,not_available,sold_out")
                .Annotation("Npgsql:Enum:menu_type", "others,main_course,drinks,dessert")
                .Annotation("Npgsql:Enum:order_status", "placed,preparing,completed,rejected,paid")
                .Annotation("Npgsql:Enum:payment_type", "cash,qr_scan,card")
                .OldAnnotation("Npgsql:Enum:entity_state", "detached,unchanged,deleted,modified,added")
                .OldAnnotation("Npgsql:Enum:menu_status", "available,not_active,not_available,sold_out")
                .OldAnnotation("Npgsql:Enum:menu_type", "others,main_course,drinks,dessert")
                .OldAnnotation("Npgsql:Enum:order_status", "placed,preparing,completed,rejected,paid")
                .OldAnnotation("Npgsql:Enum:payment_type", "cash,qr_scan,credit_card");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:entity_state", "detached,unchanged,deleted,modified,added")
                .Annotation("Npgsql:Enum:menu_status", "available,not_active,not_available,sold_out")
                .Annotation("Npgsql:Enum:menu_type", "others,main_course,drinks,dessert")
                .Annotation("Npgsql:Enum:order_status", "placed,preparing,completed,rejected,paid")
                .Annotation("Npgsql:Enum:payment_type", "cash,qr_scan,credit_card")
                .OldAnnotation("Npgsql:Enum:entity_state", "detached,unchanged,deleted,modified,added")
                .OldAnnotation("Npgsql:Enum:menu_status", "available,not_active,not_available,sold_out")
                .OldAnnotation("Npgsql:Enum:menu_type", "others,main_course,drinks,dessert")
                .OldAnnotation("Npgsql:Enum:order_status", "placed,preparing,completed,rejected,paid")
                .OldAnnotation("Npgsql:Enum:payment_type", "cash,qr_scan,card");
        }
    }
}
