import React, { useEffect, useState } from 'react';
import { useLocation, Link } from 'react-router-dom';
import './Css/ConfirmedEmail.css'; // CSS dosyasını import et

function ConfirmedEmail() {
  const [confirmationStatus, setConfirmationStatus] = useState(null);
  const location = useLocation();

  useEffect(() => {
    // URL parametrelerinden status bilgisini al
    const params = new URLSearchParams(location.search);
    const status = params.get('status');

    if (status) {
      if (status === 'success') {
        setConfirmationStatus('E-posta adresiniz başarıyla doğrulandı!');
      } else if (status === 'failure') {
        setConfirmationStatus('E-posta doğrulama işlemi başarısız oldu.');
      }
    }
  }, [location]);

  return (
    <div className="confirmation-message">
      {confirmationStatus ? (
        <>
          <h2 className={confirmationStatus.includes('başarı') ? 'confirmation-success' : 'confirmation-failure'}>
            {confirmationStatus}
          </h2>
          {confirmationStatus.includes('başarı') && (
            <p>
              Giriş yapmak için{' '}
              <Link to="/login" className="confirmation-link">
                tıklayınız.
              </Link>
            </p>
          )}
        </>
      ) : (
        <p>Yükleniyor...</p>
      )}
    </div>
  );
}

export default ConfirmedEmail;