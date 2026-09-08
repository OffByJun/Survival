using AstraNope.Data.Items;

namespace AstraNope.Contracts
{
    public interface IScanRewardService
    {
        void Grant(ScannableTarget target);
    }
}
