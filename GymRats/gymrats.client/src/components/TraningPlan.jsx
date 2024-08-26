import DownloadButton from './DownloadButton';
function TraningPlan({ title, id }) {
  return (
      <div className="traning-plan">
          <h3>{title}</h3>
          <div className="schedule">
              <p>Plan treningowy<br/> <DownloadButton fileId={id} /></p>
          </div>
      </div>
  );
}

export default TraningPlan;