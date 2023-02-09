using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using standby_data.Enums;

namespace standby_data.Models.UtilModels
{
  public class ThemeDetail
  {
    public string ID { get; set; }
    public string Text { get; set; }

    public List<ThemeDetail> ThemeDetails()
    {
      List<ThemeDetail> themes = new List<ThemeDetail>();
      themes.Add(new ThemeDetail() { ID = "material", Text = "Material" });
      themes.Add(new ThemeDetail() { ID = "material-dark", Text = "Material Dark" });
      themes.Add(new ThemeDetail() { ID = "fabric", Text = "Fabric" });
      themes.Add(new ThemeDetail() { ID = "fabric-dark", Text = "Fabric Dark" });
      themes.Add(new ThemeDetail() { ID = "tailwind", Text = "TailWind" });
      themes.Add(new ThemeDetail() { ID = "tailwind-dark", Text = "TailWind Dark" });
      themes.Add(new ThemeDetail() { ID = "bootstrap", Text = "Bootstrap" });
      themes.Add(new ThemeDetail() { ID = "bootstrap4", Text = "Bootstrap 4" });
      themes.Add(new ThemeDetail() { ID = "bootstrap-dark", Text = "Bootstrap Dark" });
      themes.Add(new ThemeDetail() { ID = "highcontrast", Text = "High Contrast" });
      themes.Add(new ThemeDetail() { ID = "bootstrap5", Text = "Boot 5" });
      themes.Add(new ThemeDetail() { ID = "bootstrap5-dark", Text = "Boot 5 - Dark" });
      themes.Add(new ThemeDetail() { ID = "fluent", Text = "Fluent" });
      themes.Add(new ThemeDetail() { ID = "fluent-dark", Text = "Fluent - Dark" });
      return themes;
    }
  }
}