export default function RegistrationFilters({
  search,
  onSearchChange,
  status,
  onStatusChange,
  type,
  onTypeChange,
  onClearFilters,
}) {
  const statuses = ["All", "Pending", "Confirmed", "CheckedIn", "Cancelled"];
  const types = ["All", "Exhibitor", "Competitor", "VIP"];

  return (
    <div className="flex flex-col gap-3">
      <div className="flex flex-col sm:flex-row gap-3 justify-between">
        <input
          type="text"
          placeholder="Search participant name..."
          value={search}
          onChange={(e) => onSearchChange(e.target.value)}
          className="border border-gray-300 rounded-lg px-4 py-2 text-sm w-full sm:w-80 bg-white"
        />

        <button
          onClick={onClearFilters}
          className="px-4 py-2 rounded-lg text-sm font-medium border border-gray-300 bg-white text-gray-600 hover:bg-gray-100"
        >
          Clear Filters
        </button>
      </div>

      <div className="flex flex-col gap-2">
        <p className="text-xs font-semibold text-gray-500">Registration Type</p>

        <div className="flex gap-2 flex-wrap">
          {types.map((t) => (
            <button
              key={t}
              onClick={() => onTypeChange(t)}
              className={`px-3 py-1.5 rounded-lg text-xs font-medium border ${
                type === t
                  ? "bg-gray-900 text-white border-gray-900"
                  : "bg-white text-gray-600 border-gray-300"
              }`}
            >
              {t}
            </button>
          ))}
        </div>
      </div>

      <div className="flex flex-col gap-2">
        <p className="text-xs font-semibold text-gray-500">Registration Status</p>

        <div className="flex gap-2 flex-wrap">
          {statuses.map((s) => (
            <button
              key={s}
              onClick={() => onStatusChange(s)}
              className={`px-3 py-1.5 rounded-lg text-xs font-medium border ${
                status === s
                  ? "bg-gray-900 text-white border-gray-900"
                  : "bg-white text-gray-600 border-gray-300"
              }`}
            >
              {s}
            </button>
          ))}
        </div>
      </div>
    </div>
  );
}
