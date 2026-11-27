import StatusBadge from './StatusBadge'
import PaymentBadge from './PaymentBadge'

export default function RegistrationTable({ items = [], loading }) {
  const formatCurrency = (amount) =>
    `₱${Number(amount ?? 0).toLocaleString('en-PH')}`

  const formatDate = (date) => {
    if (!date) return '—'

    const parsedDate = new Date(date)

    return Number.isNaN(parsedDate.getTime())
      ? '—'
      : parsedDate.toLocaleDateString('en-PH')
  }

  return (
    <div className="bg-white rounded-xl border border-gray-200 shadow-sm overflow-x-auto">
      <table className="w-full min-w-[1100px] text-left border-collapse text-sm">
        <thead className="bg-gray-50 border-b text-gray-600">
          <tr>
            <th className="p-4">Registration No.</th>
            <th className="p-4">Date</th>
            <th className="p-4">Participant</th>
            <th className="p-4">Car</th>
            <th className="p-4">Type</th>
            <th className="p-4">Status</th>
            <th className="p-4">Fee / Paid</th>
            <th className="p-4">Payment</th>
          </tr>
        </thead>

        <tbody className="divide-y">
          {loading ? (
            <tr>
              <td colSpan="8" className="p-6 text-center text-gray-400">
                Loading registrations...
              </td>
            </tr>
          ) : items.length === 0 ? (
            <tr>
              <td colSpan="8" className="p-6 text-center text-gray-400">
                No matching registrations found.
              </td>
            </tr>
          ) : (
            items.map((reg) => (
              <tr key={reg.id} className="hover:bg-gray-50">
                <td className="p-4 font-medium text-gray-900">
                  {reg.registrationNumber ?? '—'}
                </td>

                <td className="p-4">
                  {formatDate(reg.registrationDate)}
                </td>

                <td className="p-4">
                  <div className="font-semibold text-gray-900">
                    {reg.participantName ?? '—'}
                  </div>
                  <div className="text-gray-500 text-xs">
                    {reg.contactNumber ?? '—'}
                  </div>
                  <div className="text-gray-500 text-xs">
                    {reg.email ?? '—'}
                  </div>
                </td>

                <td className="p-4">
                  <div className="font-medium text-gray-900">
                    {reg.carMakeModel ?? '—'}
                  </div>
                  <div className="text-gray-500 text-xs">
                    {reg.plateNumber ?? '—'}
                  </div>
                </td>

                <td className="p-4">
                  {reg.registrationType ?? '—'}
                </td>

                <td className="p-4">
                  <StatusBadge status={reg.status} />
                </td>

                <td className="p-4 whitespace-nowrap">
                  {formatCurrency(reg.amountPaid)} /{' '}
                  <span className="text-gray-400">
                    {formatCurrency(reg.registrationFee)}
                  </span>
                </td>

                <td className="p-4">
                  <PaymentBadge status={reg.paymentStatus} />
                </td>
              </tr>
            ))
          )}
        </tbody>
      </table>
    </div>
  )
}
