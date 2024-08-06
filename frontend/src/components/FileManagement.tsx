import React, { useState, useEffect } from 'react';
import { useCookies } from 'react-cookie';
import { fetchFiles, uploadFile, deleteFile, downloadFile } from '../api/fileApi';

const FileManagement: React.FC = () => {
  const [files, setFiles] = useState<any[]>([]);
  const [cookies] = useCookies(['userId']);
  const [selectedFile, setSelectedFile] = useState<File | null>(null);

  useEffect(() => {
    const loadFiles = async () => {
      try {
        const data = await fetchFiles();
        setFiles(data);
      } catch (error) {
        console.error('Error fetching files:', error);
      }
    };

    loadFiles();
  }, []);

  useEffect(() => {
    if (selectedFile && cookies.userId) {
      const handleUpload = async () => {
        try {
          const uploadedFile = await uploadFile(selectedFile, cookies.userId.toString());
          setFiles([...files, { id: uploadedFile.id, fileName: selectedFile.name }]);
          setSelectedFile(null);
        } catch (error) {
          console.error('Error uploading file:', error);
        }
      };

      handleUpload();
    }
  }, [selectedFile, cookies.userId, files]);

  const handleDelete = async (id: number) => {
    try {
      await deleteFile(id);
      setFiles(files.filter(file => file.id !== id));
    } catch (error) {
      console.error('Error deleting file:', error);
    }
  };

  const handleDownload = async (id: number) => {
    try {
      const fileBlob = await downloadFile(id);
      const url = window.URL.createObjectURL(new Blob([fileBlob]));
      const a = document.createElement('a');
      a.href = url;
      a.download = `file_${id}`; // You can adjust the file name as needed
      document.body.appendChild(a);
      a.click();
      a.remove();
    } catch (error) {
      console.error('Error downloading file:', error);
    }
  };

  return (
    <div style={{ fontFamily: 'Arial, sans-serif', padding: '20px', maxWidth: '800px', margin: '0 auto' }}>
      <h1 style={{ textAlign: 'center' }}>File Management</h1>
      <div style={{ marginBottom: '20px', display: 'flex', flexDirection: 'column', alignItems: 'center' }}>
        <input
          type="file"
          onChange={(e) => {
            if (e.target.files) {
              setSelectedFile(e.target.files[0]);
            }
          }}
          style={{ marginBottom: '10px' }}
        />
      </div>
      <ul style={{ listStyleType: 'none', padding: '0' }}>
        {files.map(file => (
          <li key={file.id} style={{ marginBottom: '10px', padding: '10px', border: '1px solid #ddd', borderRadius: '5px' }}>
            <div style={{ display: 'flex', alignItems: 'center' }}>
              <span style={{ flexGrow: 1 }}>{file.fileName}</span>
              <button 
                onClick={() => handleDelete(file.id)}
                style={{ backgroundColor: '#dc3545', color: '#fff', border: 'none', padding: '5px 10px', cursor: 'pointer', marginRight: '5px' }}
              >
                Delete
              </button>
              <button 
                onClick={() => handleDownload(file.id)}
                style={{ backgroundColor: '#007bff', color: '#fff', border: 'none', padding: '5px 10px', cursor: 'pointer' }}
              >
                Download
              </button>
            </div>
          </li>
        ))}
      </ul>
    </div>
  );
};

export default FileManagement;
