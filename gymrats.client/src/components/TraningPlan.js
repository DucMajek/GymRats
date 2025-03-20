import DownloadButton from './DownloadButton';
import '../assets/styles/DietPlan.css';
function TraningPlan({ title, id }) {
  return (
      <div className="traning-plan">
          <h3>{title}</h3>
          <div className="schedule">
              <p><DownloadButton useAlternativeApi={true} fileId={id}/></p>
          </div>
      </div>
  );
}

export default TraningPlan;