import Header from '../components/Header';
import Sidebar from '../components/Sidebar';
function UserProfile(){
    return (
        <div className="main">
          <Header />
          <div className="content-wrapper">
            <Sidebar />
            <main className="main-content">
              <div className='personalData'>
                <h2>Personal Data</h2>
              </div>
              <div className='userBmi' >
                <h2>BMI</h2>
              </div>
            </main>
          </div>
        </div>
      );

}

export default UserProfile;