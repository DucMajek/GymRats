import Sidebar from './Sidebar';
import {useState} from 'react'

function CollapsibleSidebar() {
    const [sidebarOpen, setSideBarOpen] = useState(false);
    const handleViewSidebar = () => {
      setSideBarOpen(!sidebarOpen);
    };
    return (
      <span>
        <Sidebar isOpen={sidebarOpen} toggleSidebar={handleViewSidebar} />
      </span>
    );
  }

  export default CollapsibleSidebar;
  