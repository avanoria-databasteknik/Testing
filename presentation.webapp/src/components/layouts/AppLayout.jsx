import React from 'react'
import { Outlet } from 'react-router-dom'
import Footer from '../sections/Footer'
import Header from '../sections/Header'

const AppLayout = () => {
  return (
    <div className="app-layout">
        <Header />
        <main>
            <Outlet />
        </main>
        <Footer />
    </div>
  )
}

export default AppLayout