import { useEffect, useState } from 'react'
import { getRegistrations, getSummary } from '../api/sample-data'
import KpiCards from '../components/KpiCards'
import Pagination from '../components/Pagination'
import RegistrationFilters from '../components/RegistrationFilters'
import RegistrationTable from '../components/RegistrationTable'
import { useDebounce } from '../hooks/useDebounce'

const emptyKpis = {
  totalRegistrations: 0,
  totalPayments: 0,
  uncollectedPayments: 0,
  confirmed: 0,
  checkedIn: 0,
}

export default function RegistrationsPage() {
  const [search, setSearch] = useState('')
  const [status, setStatus] = useState('All')
  const [type, setType] = useState('All')
  const [page, setPage] = useState(1)
  const [pageSize, setPageSize] = useState(5)

  const [registrations, setRegistrations] = useState({
    items: [],
    totalCount: 0,
    totalPages: 0,
    kpis: emptyKpis,
  })

  const [loading, setLoading] = useState(true)
  const [error, setError] = useState('')

  const debouncedSearch = useDebounce(search)

  useEffect(() => {
    let active = true

    async function loadDashboard() {
      try {
        setLoading(true)
        setError('')

        const [registrationResult, summaryResult] = await Promise.all([
          getRegistrations({
            participantName: debouncedSearch,
            status,
            type,
            page,
            pageSize,
          }),
          getSummary(),
        ])

        if (active) {
          setRegistrations({
            ...registrationResult,
            kpis: summaryResult,
          })
        }
      } catch (requestError) {
        if (active) {
          setError(
            requestError instanceof Error
              ? requestError.message
              : 'Unable to load registrations.',
          )
        }
      } finally {
        if (active) {
          setLoading(false)
        }
      }
    }

    loadDashboard()

    return () => {
      active = false
    }
  }, [debouncedSearch, status, type, page, pageSize])

  const updateSearch = (value) => {
    setSearch(value)
    setPage(1)
  }

  const updateStatus = (value) => {
    setStatus(value)
    setPage(1)
  }

  const updateType = (value) => {
    setType(value)
    setPage(1)
  }

  const updatePage = (value) => {
    setPage(value)
  }

  const updatePageSize = (value) => {
    setPageSize(value)
    setPage(1)
  }

  const clearFilters = () => {
    setSearch('')
    setStatus('All')
    setType('All')
    setPage(1)
  }

  return (
    <main className="registrations-page">
      <header className="registrations-header">
        <h1>Car Show 2027</h1>
        <p>Manage event registrations, payments, and check-ins.</p>
      </header>

      <KpiCards kpis={registrations.kpis ?? emptyKpis} />

      <section
        className="registrations-list"
        aria-label="Registration list"
      >
        <RegistrationFilters
          search={search}
          onSearchChange={updateSearch}
          status={status}
          onStatusChange={updateStatus}
          type={type}
          onTypeChange={updateType}
          onClearFilters={clearFilters}
        />

        {error && (
          <p role="alert" className="registrations-error">
            Failed to load registrations: {error}
          </p>
        )}

        {/* Only the registration table scrolls */}
        <div className="h-[420px] overflow-y-auto overflow-x-auto">
          <RegistrationTable
            items={registrations.items ?? []}
            loading={loading}
          />
        </div>

        {/* Pagination stays outside the scrollable area */}
        <Pagination
          page={page}
          pageSize={pageSize}
          totalCount={registrations.totalCount ?? 0}
          totalPages={registrations.totalPages}
          onPageChange={updatePage}
          onPageSizeChange={updatePageSize}
        />
      </section>
    </main>
  )
}