import Header from '../components/Header';
import Sidebar from '../components/Sidebar';
import '../assets/styles/UserProfile.css'
function UserProfile() {
  return (
    <div className="main">
      <Header />
      <div className="content-wrapper">
        <Sidebar />
        <main className="main-content">
          <div className='personalData'>
            <section>
              <div className='myData'>
                <h5>Moje dane</h5>
                <button>zmień hasło</button>
                <button>edytuj</button>
              </div>
              <p>Imie Nazwisko</p>
              <p>email</p>
              <p>telefon</p>
              <p>plec</p>
            </section>
            <section>
              <h5>Adres kontaktowy</h5>
              <p>Imie Nazwisko</p>
              <p>Adres</p>
              <p>Kod pocztowy, miasto</p>
            </section>
          </div>
          <div className='userBmi' >

          </div>
        </main>
      </div>
    </div>
  );

}

export default UserProfile;