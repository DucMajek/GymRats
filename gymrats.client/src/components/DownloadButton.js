import axios from 'axios';

const DownloadButton = ({ useAlternativeApi, fileId, calories, type }) => {
    const downloadFile = async () => {
        const dietUrl = `https://localhost:44380/trainingPlan/${type}/${calories}`;
        const traningPlanUrl = `https://localhost:44380/diet/${fileId}`;
        const url = useAlternativeApi ? traningPlanUrl : dietUrl;

        try {
            const response = await axios.get(url, { responseType: 'blob', credentials: true });
            const urlBlob = window.URL.createObjectURL(new Blob([response.data]));
            const link = document.createElement('a');
            link.href = urlBlob;

            const contentDisposition = response.headers['content-disposition'];
            let fileName;

            if (contentDisposition) {
                const fileNameEncodedMatch = contentDisposition.match(/filename\*=UTF-8''([^;]+)/i);
                if (fileNameEncodedMatch && fileNameEncodedMatch[1]) {
                    fileName = decodeURIComponent(fileNameEncodedMatch[1]);
                } else {
                    const fileNameMatch = contentDisposition.split('filename=')[1];
                    if (fileNameMatch) {
                        fileName = fileNameMatch.replace(/["';]/g, '').trim();
                    }
                }
            }

            /*if (useAlternativeApi) {
                fileName = fileName + '.pdf';
            } else {
                fileName = fileName + '.pdf';
            }*/

            fileName = fileName + ".pdf"

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