import Foundation

final class SecurityGuard {

    private static let kPtraceDenyAttachRequest: CInt = 31
    private static let kSystemCLibraryPath = "/usr/lib/libc.dylib"
    private static let kPtraceFunctionName = "ptrace"
    private static let kExpectedBundleIdentifier = "com.cajatacna.appmovil"
    private static let kSysctlSuccess: Int32 = 0

    static func performChecks() {
        #if !DEBUG
        denyDebuggerAttachment()
        if isDebuggerAttached() {
            fatalError()
        }
        if !validateBundleIntegrity() {
            fatalError()
        }
        #endif
    }

    private static func denyDebuggerAttachment() {
        let handle = dlopen(kSystemCLibraryPath, RTLD_NOW)
        if let ptracePtr = dlsym(handle, kPtraceFunctionName) {
            typealias PtraceType = @convention(c) (CInt, CInt, CInt, CInt) -> CInt
            let ptrace = unsafeBitCast(ptracePtr, to: PtraceType.self)
            _ = ptrace(kPtraceDenyAttachRequest, 0, 0, 0)
        }
        dlclose(handle)
    }

    private static func isDebuggerAttached() -> Bool {
        var info = kinfo_proc()
        var size = MemoryLayout<kinfo_proc>.stride
        var mib: [Int32] = [CTL_KERN, KERN_PROC, KERN_PROC_PID, getpid()]
        let result = sysctl(&mib, UInt32(mib.count), &info, &size, nil, 0)
        if result != kSysctlSuccess { return false }
        return (info.kp_proc.p_flag & P_TRACED) != 0
    }

    private static func validateBundleIntegrity() -> Bool {
        guard let bundleId = Bundle.main.bundleIdentifier else { return false }
        return bundleId == kExpectedBundleIdentifier
    }
}
