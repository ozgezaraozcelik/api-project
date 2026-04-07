using PersonnelTrainingAPI.Models;

namespace PersonnelTrainingAPI.Data
{
    public static class DataStore
    {
        public static List<Personnel> Personnels { get; } =
        [
            new Personnel
            {
                Id = 1,
                FirstName = "Ayşe",
                LastName = "Yılmaz",
                Department = "İK",
                Email = "ayse.yilmaz@company.local",
                IsTrainingCompleted = true,
                JoinDate = DateTime.UtcNow.AddDays(-420)
            },
            new Personnel
            {
                Id = 2,
                FirstName = "Mehmet",
                LastName = "Demir",
                Department = "Yazılım",
                Email = "mehmet.demir@company.local",
                IsTrainingCompleted = false,
                JoinDate = DateTime.UtcNow.AddDays(-210)
            },
            new Personnel
            {
                Id = 3,
                FirstName = "Elif",
                LastName = "Kaya",
                Department = "Finans",
                Email = "elif.kaya@company.local",
                IsTrainingCompleted = true,
                JoinDate = DateTime.UtcNow.AddDays(-95)
            },
            new Personnel
            {
                Id = 4,
                FirstName = "Can",
                LastName = "Şahin",
                Department = "Satış",
                Email = "can.sahin@company.local",
                IsTrainingCompleted = false,
                JoinDate = DateTime.UtcNow.AddDays(-60)
            },
            new Personnel
            {
                Id = 5,
                FirstName = "Zeynep",
                LastName = "Aydın",
                Department = "Operasyon",
                Email = "zeynep.aydin@company.local",
                IsTrainingCompleted = true,
                JoinDate = DateTime.UtcNow.AddDays(-30)
            }
        ];
    }
}

