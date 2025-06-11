import DownloadButton from './DownloadButton';
import '../assets/styles/DietPlan.css';
function TraningPlan({ title, id, onDelete, isAdmin }) {
  return (
      <div className="traning-plan">
          <h3>{title}</h3>
          <div className="schedule">
              <p>
                <DownloadButton useAlternativeApi={true} fileId={id}/>
                {isAdmin && (
                  <button style={{marginLeft: 8, background: 'red'}} onClick={() => onDelete(id)}>Usuń</button>
                )}
              </p>
          </div>
      </div>
  );
}

export default TraningPlan;