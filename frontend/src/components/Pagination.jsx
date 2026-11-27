export default function Pagination({
  page,
  pageSize,
  totalCount,
  totalPages,
  onPageChange,
  onPageSizeChange,
}) {
  const calculatedTotalPages = Math.max(
    1,
    totalPages ?? Math.ceil(totalCount / pageSize),
  )

  const startItem = totalCount === 0 ? 0 : (page - 1) * pageSize + 1
  const endItem = Math.min(page * pageSize, totalCount)

  return (
    <div className="flex flex-col sm:flex-row items-center justify-between gap-4 text-sm text-gray-600">
      <p>
        Showing {startItem}–{endItem} of {totalCount} registrations
      </p>

      <div className="flex items-center gap-2">
        <label htmlFor="page-size">Rows per page:</label>

        <select
          id="page-size"
          value={pageSize}
          onChange={(e) => onPageSizeChange(Number(e.target.value))}
          className="border border-gray-300 rounded-lg px-2 py-1 bg-white"
        >
          <option value={5}>5</option>
          <option value={10}>10</option>
          <option value={20}>20</option>
        </select>
      </div>

      <div className="flex items-center gap-2">
        <button
          disabled={page <= 1}
          onClick={() => onPageChange(page - 1)}
          className="px-3 py-1 border rounded-lg bg-white disabled:opacity-40"
        >
          Previous
        </button>

        {Array.from(
          { length: calculatedTotalPages },
          (_, index) => index + 1,
        ).map((pageNumber) => (
          <button
            key={pageNumber}
            onClick={() => onPageChange(pageNumber)}
            aria-current={page === pageNumber ? 'page' : undefined}
            className={`px-3 py-1 border rounded-lg ${
              page === pageNumber
                ? 'bg-gray-900 text-white border-gray-900'
                : 'bg-white text-gray-600'
            }`}
          >
            {pageNumber}
          </button>
        ))}

        <button
          disabled={page >= calculatedTotalPages}
          onClick={() => onPageChange(page + 1)}
          className="px-3 py-1 border rounded-lg bg-white disabled:opacity-40"
        >
          Next
        </button>
      </div>
    </div>
  )
}
