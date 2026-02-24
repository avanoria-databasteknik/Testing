import React from 'react'
import { Outlet } from 'react-router-dom'
import AdminHeader from '../sections/AdminHeader'
import AdminSidebar from '../sections/AdminSidebar'

const AdminLayout = () => {
  return (
    <div className="admin-layout">
        <AdminHeader />
        <AdminSidebar />
        <main>
            <Outlet />
        </main>
    </div>
  )
}

export default AdminLayout