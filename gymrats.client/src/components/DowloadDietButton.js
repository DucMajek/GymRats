import React from 'react';
import axios from 'axios';
//import { saveAs } from 'file-saver';
const DowloadDietButton = ({ fileId, calories }) => { 
    const downloadFile = () => {
        
        axios({
            url: `https://localhost:7155/api/Files/downloadPdfFile/${fileId}/${calories}`,
            method: 'GET',
            responseType: 'blob',
        })
            .then((response) => {
                console.log(response);
                const url = window.URL.createObjectURL(new Blob([response.data]));
                const link = document.createElement('a');
                link.href = url;

                // Sprawdzenie czy naglowek 'content-disposition' istnieje
                const contentDisposition = response.getHeaders('content-disposition');
                
                let fileName = 'asd.pdf';
                

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
            })
            .catch((error) => console.error('Blad podczas pobierania pliku', error));
    };


    return (
        <button onClick={downloadFile}>
            Pobierz pdf
        </button>
    );
};

export default DowloadDietButton;