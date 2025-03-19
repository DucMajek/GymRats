import axios from 'axios';

const DownloadButton = ({ useAlternativeApi, fileId, calories, type }) => {
    const downloadFile = async () => {
        const dietUrl = `https://localhost:44380/DownloadDietFile/${type}/${calories}`;
        const traningPlanUrl = `https://localhost:44380/DownloadTranningPlanFile/${fileId}`;
        const url = useAlternativeApi ? traningPlanUrl : dietUrl;

        try {
            const response = await axios.get(url, { responseType: 'blob' });
            const urlBlob = window.URL.createObjectURL(new Blob([response.data]));
            const link = document.createElement('a');
            link.href = urlBlob;
		 

            const contentDisposition = response.headers['content-disposition'];
            let fileName;

            if (useAlternativeApi) {
                fileName = 'Plan treningowy.xlsx';
            } else {
                fileName = 'dieta.pdf';
            }

            if (contentDisposition) {
                const fileNameMatch = contentDisposition.split('filename=')[1];
                if (fileNameMatch) {
                    fileName = fileNameMatch.replace(/"/g, '');
                }
            }

            link.setAttribute('download', fileName);
            document.body.appendChild(link);
            link.click();
            link.remove();
        } catch (error) {
            console.error('Błąd podczas pobierania pliku', error);
        }
    };

    return (
        <button onClick={downloadFile}>
            Pobierz plik
        </button>
					 
    );
	 
};

export default DownloadButton;