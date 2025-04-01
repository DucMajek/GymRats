import GymRats_logo from '../assets/img/GymRats_Logo.png';
import '../assets/styles/HomePage.css';
import 'bootstrap/dist/js/bootstrap.bundle.min';
function Navbar() {
  
    const scrollToSection = (id) => {
        const element = document.getElementById(id);
        if (element) {
            element.scrollIntoView({ behavior: "smooth" });
        }
    };
    return (
        <div className='stickyNavbar'>
            <nav className="navbar navbar-expand-lg">
                <div className="container-fluid">
                    <a className="navbar-brand">
                        <img src={GymRats_logo} alt="gymRatsLogo_resized" className="img-logo" />
                    </a>
                    <button
                        className="navbar-toggler"
                        type="button"
                        data-bs-toggle="collapse"
                        data-bs-target="#navbarNavDropdown"
                        aria-controls="navbarNavDropdown"
                        aria-expanded="false"
                        aria-label="Toggle navigation"
                    >
                        <span className="navbar-toggler-icon"></span>
                    </button>
                    <div className="collapse navbar-collapse" id="navbarNavDropdown" >
                        <ul className="navbar-nav">
                            <li className="nav-item">
                                <a className="nav-link hover-orange fontColor" href="/">Strona Główna</a>
                            </li>
                            <li className="nav-item">
                                <a
                                    className="nav-link hover-orange fontColor" 
                                    href="#"
                                    onClick={(e) => { e.preventDefault(); scrollToSection("price__grid"); }}
                                >
                                    Karnety
                                </a>
                            </li>
                            <li className="nav-item">
                                <a
                                    className="nav-link hover-orange fontColor"
                                    href="#"
                                    onClick={(e) => { e.preventDefault(); scrollToSection("courses"); }}
                                >
                                    Kursy
                                </a>
                            </li>
                            <li className="nav-item">
                                <a
                                    className="nav-link hover-orange fontColor"
                                    href="#"
                                    onClick={(e) => { e.preventDefault(); scrollToSection("about__us"); }}
                                >
                                    O nas
                                </a>
                            </li>
                            <li className="nav-item">
                                <a className="nav-link hover-orange fontColor" href="/login">Konto</a>
                            </li>
                        </ul>
                    </div>
                </div>
            </nav>
        </div>
    );
}

export default Navbar;