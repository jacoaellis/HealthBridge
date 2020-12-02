using HealthBridgeClinical.Models.DTOs.Common;

namespace HealthBridgeClinical.Models.DTOs
{
    public class ContinentsDto
    {
        public string Continent { get; set; }
        public CommonDataDto New { get; set; }
        public CommonDataDto Active { get; set; }
        public CommonDataDto Deaths { get; set; }
    }

}
