import axios from 'axios';

const DownloadButton = ({ useAlternativeApi, fileId, calories }) => {
    const downloadFile = async () => {
        const dietUrl = `https://localhost:7200/api/Files/downloadDietFile/${fileId}/${calories}`;
        const traningPlanUrl = `https://localhost:7200/api/Files/downloadTranningPlanFile/${fileId}`;
        const url = useAlternativeApi ? traningPlanUrl : dietUrl;

        try {
            const response = await axios.get(url, { responseType: 'blob' });
            const urlBlob = window.URL.createObjectURL(new Blob([response.data]));
            const link = document.createElement('a');
            link.href = urlBlob;

            // Próbujemy wyciągnąć nazwę pliku z nagłówka
            const contentDisposition = response.headers['content-disposition'];
            let fileName;
            if(!url){
                 fileName = 'dieta.pdf';
            } else {
                 fileName = 'Plan treningowy.xlsx';
            }
                 // Domyślna nazwa pliku

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
