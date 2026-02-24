import React from 'react'
import { Outlet } from 'react-router-dom'
import Footer from '../sections/Footer'

const BlankLayout = () => {
  return (
    <div className="blank-layout">
        <main>
            <Outlet />
        </main>
        <Footer />
    </div>
  )
}

export default BlankLayout